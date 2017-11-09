/*****************************************
系统帮助类 主要用于表格初始化
ui初始化
*****************************************/

(function ($) {
    $.extend({
    });
})(jQuery);

var jqGrid = {
    grid: null,
    jqGridTableWidth: "",
    postData: {},
    fm: "",
    url: "",
    isPage: false,
    Guid: function () {
        function G() {
            return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1)
        }
        return (G() + G() + "-" + G() + "-" + G() + "-" +
             G() + "-" + G() + G() + G());
    },

    gAjax: function (url, data, callbackFun, errorCallbackFun) {
        //jNotify.showLoading();
        $.ajax({
            url: url,
            type: "post",
            cache: false,
            data: data,
            success: function (result) {
                //jNotify.hideLoading();
                if (callbackFun) {
                    callbackFun(result);
                }
            },
            error: function (result) {
                //jNotify.hideLoading();
                if (errorCallbackFun) {
                    errorCallbackFun(result);
                } else {
                    jNotify.showError("服务器繁忙，请稍后重试");
                }
            }
        });
    },

    getfrmData: function () {
        var s = jqGrid.grid.jqGrid("getGridParam", "postData");
        jqGrid.postData = {
            "_search": true,
            "nd": Math.random(),
            "rows": s.rows,
            "page": jqGrid.isPage ? s.page : 1,
            "sidx": s.sidx,
            "sord": s.sord
        };
        if (jqGrid.fm !== "") {
            var formVal = jqGrid.fm.serialize().replace(/\+/g, " ");;
            formVal = decodeURIComponent(formVal, true);
            var formArray = formVal.split("&");

            for (var index in formArray) {
                if (formArray.hasOwnProperty(index)) {
                    var item = formArray[index].split("=");
                    if (item[1] !== "") {
                        jqGrid.postData[item[0]] = item[1];
                    }
                }
            }
        }

    },
    gridSearch: function (fm, url) {
        jqGrid.fm = fm;
        jqGrid.url = url;
        jqGrid.getfrmData();
        //jqGrid.postData.sidx = six;
        //if (sod == undefined) {
        //    sod = "desc";
        //}
        //jqGrid.postData.sord = sod;
        jqGrid.gAjax(jqGrid.url, jqGrid.postData, function (res) {
            jqGrid.grid[0].addJSONData(res);
        });
    },

    collectAjax: function (fm, url, callbackFun) {
        jqGrid.fm = fm;
        jqGrid.getfrmData();

        jqGrid.gAjax(url, jqGrid.postData, callbackFun);
    },

    gridDelete: function (options) {
        var deleteKeys = "";
        var selRows = jqGrid.grid.jqGrid("getGridParam", "selarrrow");
        if (selRows.length === 0) {
            jNotify.showError("请选择需要删除的行");
            return false;
        }
        jNotify.Confirm("", "Please confirm to delete the item.",
            function () {
                for (var index = 0; index < selRows.length; index++) {
                    deleteKeys += jqGrid.grid.jqGrid("getRowData", selRows[index])[options.KeyName] + ",";
                }
                deleteKeys = deleteKeys.substring(0, deleteKeys.length - 1);
                if (deleteKeys === "") return false;

                jForm.Ajax({
                    url: options.url,
                    data: { deleteKeys: deleteKeys },
                    success: function (data) {
                        jqGrid.grid.jqGrid().trigger("reloadGrid");
                    }

                });
            });
        return false;
    },
    gridConfirm: function (options) {
        var confirmKeys = "";
        var selRows = jqGrid.grid.jqGrid("getGridParam", "selarrrow");
        if (selRows.length === 0) {
            jNotify.showError("请选择需要选择的行");
            return false;
        }
        jNotify.Confirm("确认提示", "确定要选择该部门吗？",
            function () {
                for (var index = 0; index < selRows.length; index++) {
                    confirmKeys += jqGrid.grid.jqGrid("getRowData", selRows[index])[options.KeyName] + ",";
                }
                confirmKeys = confirmKeys.substring(0, confirmKeys.length - 1);
                if (confirmKeys === "") return false;

                jForm.Ajax({
                    url: options.url,
                    data: { confirmKeys: confirmKeys },
                    success: function (data) {
                        jqGrid.grid.jqGrid().trigger("reloadGrid");
                    }

                });
            });
        return false;
    },

    initGrid: function (grid, options) {
        jqGrid.fm = "";
        jqGrid.grid = grid;
        var p = $.extend({
            mtype: "post", //ajax提交方式
            cache: false,
            datatype: "json",
            //从服务器端返回的数据类型，默认xml。可选类型：xml，local，json，jsonnp，script，xmlstring，jsonstring，clientside
            autowidth: true,
            //如果为ture时，则当表格在首次被创建时会根据父元素比例重新调整表格宽度。如果父元素宽度改变，为了使表格宽度能够自动调整则需要实现函数：setGridWidth
            height: "auto", //表格高度，可以是数字，像素值或者百分比
            shrinkToFit: true, //此属性用来说明当初始化列宽度时候的计算类型，如果为true，则按比例初始化列宽度。如果为false，则列宽度使用colModel指定的宽度
            autoScroll: false,
            rowNum: 10, //	在grid上显示记录条数，这个参数是要被传递到后台
            rowList: [10, 25, 50, 100], //一个下拉选择框，用来改变显示记录数，当选择时会覆盖rowNum参数传递到后台
            pager: "#pager-content", //	定义翻页用的导航栏，必须是有效的html元素。翻页工具栏可以放置在html页面任意位置
            viewrecords: true, //定义是否要显示总记录数
            hidegrid: false, //启用或者禁用控制表格显示、隐藏的按钮，只有当caption 属性不为空时起效
            multiselectWidth: 45,
            onPaging: function () {
                jqGrid.isPage = true;
            },
            jsonReader: {
                root: "msgBody.RowsData", //数据模型
                page: "msgBody.page", //数据页码
                total: function (res) {
                    var total = res.msgBody.Total % res.msgBody.rows > 0
                        ? res.msgBody.Total / res.msgBody.rows + 1
                        : res.msgBody.Total / res.msgBody.rows;
                    return total;
                }, //数据总页码
                records: "msgBody.Total", //数据总记录数
                repeatitems: false
            }, //描述json 数据格式的数组
            beforeRequest: function () {
                if (jqGrid.grid != null) {
                    jqGrid.gridSearch(jqGrid.fm, options.url);
                    return false;
                }
            },
            gridComplete: function () {
                jqGrid.jqGridTableWidth = grid.width();
                jqGrid.setJqGridWidth();
                $(window).on('resize', function () { jqGrid.setJqGridWidth() });
                $(".navbar-header")
                    .on("click",
                        function (e) {
                            if ($(e.target).is(".navbar-minimalize") || $(e.target).parent().is(".navbar-minimalize")) {
                                setTimeout(function () { jqGrid.setJqGridWidth() }, 500);
                            }
                        });
                //$(":checkbox").addClass("i-checks");
                jqGrid.isPage = false;
                $(".i-checks")
                    .iCheck({
                        checkboxClass: "icheckbox_square-green",
                        radioClass: "iradio_square-green"
                    });
            },
            loadError: function (xhr, status, error) {
                jNotify.showError(xhr.responseText);
            }
        }, options ? options : {});
        return grid.jqGrid(p);
    },

    setJqGridWidth: function () {
        var width = $('.jqGrid_wrapper').width();
        if (width > jqGrid.jqGridTableWidth) {
            jqGrid.grid.setGridWidth(width, true);
        } else {
            jqGrid.grid.setGridWidth(width, false);
        }
    }

};
$(function () {

});