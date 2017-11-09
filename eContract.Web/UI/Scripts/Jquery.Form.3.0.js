/*****************************************
系统帮助类 主要包含信息提示form 验证
ui初始化
版权所有：王海龙
*****************************************/

///获取系统当前日期时间
Date.prototype.format = function (format) {
    var o = {
        "M+": this.getMonth() + 1, //month 
        "d+": this.getDate(), //day 
        "h+": this.getHours(), //hour 
        "m+": this.getMinutes(), //minute 
        "s+": this.getSeconds(), //second 
        "q+": Math.floor((this.getMonth() + 3) / 3), //quarter 
        "S": this.getMilliseconds() //millisecond 
    }
    if (format.indexOf("H") != -1) {
        o = {
            "M+": this.getMonth() + 1, //month 
            "d+": this.getDate(), //day 
            "H+": this.getHours(), //hour 
            "m+": this.getMinutes(), //minute 
            "s+": this.getSeconds(), //second 
            "q+": Math.floor((this.getMonth() + 3) / 3), //quarter 
            "S": this.getMilliseconds() //millisecond 
        }
    }
    if (/(y+)/.test(format)) {
        format = format.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    }
    for (var k in o) {

        if (new RegExp("(" + k + ")").test(format)) {
            format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
        }
    }
    return format;
}
var jNotify = {
    Notify: function (type, options) {
        swal($.extend({
            title: options.Title ? options.Title : "",
            text: options.Content,
            type: type,
            showCancelButton: false,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "OK",
            cancelButtonText: "Cancel",
            closeOnConfirm: true,
            closeOnCancel: true
        }, options ? options : {}), function (isConfirm) {

            if (isConfirm) {
                if (options.Ok)
                    options.Ok();
            }
            else {
                if (options.Cancel) {
                    options.Cancel();
                }
            }
        });
    },
    //预加载图片
    prevLoadImage: function (rootpath, paths) {
        for (var i in paths) {
            $('<img />').attr('src', rootpath + paths[i]);
        }
    },
    //消息提示
    Alert: function (title, content, Ok) {
        this.Notify("", { Title: title, Content: content, Ok: Ok, IsError: (Ok ? true : false) });
    },
    //显示loading
    showLoading: function (message) {
        message = message || "正在加载中...";
        jNotify.Alert("", "" + message);
        // $.ligerui.win.mask();
        //this.Notify("", { imageUrl: "/UI/INSPINIA/img/gif/loading.gif"});
    },
    //隐藏loading
    hideLoading: function (message) {
        jModal = $(".showSweetAlert");
        //jModal = $("#load-notify");
        if (jModal != null) {
            swal.close();
            jModal.slideUp();
        }
    },
    //显示成功提示窗口
    showSuccess: function (message, Ok) {
        if (typeof (message) == "function" || arguments.length == 0) {
            callback = message;
            message = "操作成功!";
        }
        this.Notify("success", { Title: "", Content: message, Ok: Ok, IsError: (Ok ? true : false) }); //Title成功提示，去掉
      
    },
    //显示失败提示窗口
    showError: function (message, ok, Cancel, options) {
        if (typeof (message) == "function" || arguments.length == 0) {
            callback = message;
            message = "操作失败!";
        }
        this.Notify("error", $.extend({ Title: "", Content: message, Ok: ok, IsError: true }, { Params: options }));//Title错误提示，去掉
    },
    //警告
    Warn: function (content, title, fn) {
        this.Notify("warning", { Title: "系统警告", Content: content, Ok: fn, IsError: (Ok ? true : false) });
    },
    //问题描述
    Question: function (content, title, fn) {
        this.Notify("warning", { Title: "信息提示", Content: content, Ok: fn, IsError: (Ok ? true : false) });
    },

    Confirm: function (title, content, fn, fn1, options) {
        this.Notify("warning", $.extend({ Title: title, Content: content, Ok: fn, Cancel: fn1, Confirm: true, showCancelButton: true }, { Params: options }));


    },
    Close: function () {
        jModal = $("#load-notify");
        if (jModal != null) {
            jModal.slideUp();
        }
    },
    //跳转
    TargetUrl: function (url) {
        if (url && url != "") {
            jNotify.Alert("", "<i class='icon-spinner icon-spin icon-large'></i>" + SysLanguage.lbl_PageBeing);
            window.location.href = url;
        }
    }

}

Array.prototype.InArray = function (e) {
    for (i = 0; i < this.length && this[i] != e; i++);
    return !(i == this.length);
}
var arrDialog = {};
var jForm = {
    Guid: function () {
        function G() {
            return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1)
        }
        return (G() + G() + "-" + G() + "-" + G() + "-" +
             G() + "-" + G() + G() + G());
    },

    // 将/Date()/格式字符串转换成datetime
    DateParse: function (str) {
        var d = str.replace(/^\//, "new ").replace(/\/$/, "");
        d = eval(d);
        return d;
    },
    xValidate: function (form, options) {
        try {
            var rule = {};
            var messages = {};
            //,.radioboxlist[validate],.checkboxlist[checkboxlist]
            form.find("input[validate],textarea[validate],select[validate]").each(function () {
                var name = ($(this).attr("key") ? $(this).attr("key") : $(this).attr("name"));
                rule[name] = eval("(" + $(this).attr("validate") + ")");

            });
            options = $.extend({
                onsubmit: !form.attr("onsubmit"),
                meta: "validate",
                onKeyup: true,
                onfocusout: function (element) {
                    $(element).valid();
                },
                rules: rule,
                ignore: options.ignore ? options.ignore : ".ignore",
                errorPlacement: function (lable, element) {
                    var message = lable.html();
                    if (message == "该属性必填，不可为空！") {
                        if (element.closest(".form-group").children("label").hasClass("control-label")) {
                            message = element.closest(".form-group").children(".control-label").text().replace("：", "").replace("*", "") + " is required!";
                        }
                    }
                    element.closest(".form-group").find(".help-block").html(message);
                },
                success: function (lable) {
                    var element = $("#" + lable.attr("for"));
                    if (element.closest(".form-group").hasClass("has-error")) {
                        element.closest(".form-group").removeClass("has-error");
                    }
                    element.closest(".form-group").find("p.help-block").html("");
                }

            }, options || {});
            if (!form.validate(options)) {
                return false;
            }

            if (options && options.afterCheck) {
                return options.afterCheck(form, options);
            }
            return true;
        }
        catch (e) {
            jNotify.Alert(e);
            return false;
        }
    },
    //获取表单的直
    getfrmData: function (frm) {
        var data = {};
        frm.find("input,select,textarea").each(function () {
            var fN = $(this).attr("name");
            if (!fN || fN == "") {
                fN = $(this).attr("Id");
            }
            if ($(this).attr("type") == "select") {
                data[fN] = $(this).next().find("input[data-ligerid='" + $(this).attr("id") + "']").val();
            }
            else if ($(this).attr("type") == "checkbox") {
                data[fN] = $(this)[0].checked;
            }
            else {
                data[fN] = $(this).val();
            }
        });
        return data;
    },
    Ajax: function (options) {
        $.ajax({
            cache: false,
            async: true,
            url: options.url,
            data: options.data,
            dataType: 'json',
            type: 'POST',
            timeout: 100000,
            beforeSend: function () {
                if (!options.IsNoLoad) {
                    jNotify.showLoading();
                }
            },
            complete: function (xhr, textStatus) {

                if (xhr.responseText && xhr.responseText != null && xhr.responseText.indexOf("The return URL specified for request redirection is invalid.") != -1) {
                    window.location.href = window.location.href;
                }
                if (options.complete) {
                    options.complete(xhr, textStatus, options);
                }

            },
            success: function (result) {
                //if (!options.IsNoLoad) {
                //    jNotify.hideLoading();
                //}
                if (!result) return;

                if (!result.msgHeader.IsError) {
                    if (options.success) {
                        options.success(result);
                        //options.success(result.msgBody, result.msgHeader.Message);
                    } else {
                        jNotify.showSuccess(result.msgHeader.Message == null ? "操作成功" : result.msgHeader.Message, "");
                    }
                }
                else {
                    if (options.error)
                        options.error(result);
                    else {
                        jNotify.showError(result.msgHeader.Message == null ? "操作失败" : result.msgHeader.Message, "");
                    }
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                // jNotify.Error("", XMLHttpRequest.statusCode + ":" + XMLHttpRequest.readyState + ":" + XMLHttpRequest.responseText + ":" + XMLHttpRequest.statusText);
                if (!options.IsNoLoad) {
                    jNotify.Close();
                }
            }

        });
    },
    initGrid: function (grid, options) {

        var p = $.extend({
            pageLength: 20,
            responsive: true,
            info: false,
            aLengthMenu: [20, 30, 50, 100, 200, 500, 1000], //更改显示记录数选项
            bProcessing: true,
            bServerSide: true, //是否启动服务器端数据导入  
            bSortClasses: true,

            bScrollCollapse: true,

            columnDefs: [
               { "orderable": false, "targets": 0 }
            ],

            language: {
                search: "搜索：",
                lengthMenu: "每页显示 _MENU_ 条记录",

                //sZeroRecords: "没有检索到数据",
                //sProcessing: "正在获取数据，请稍后...",
                paginate: {
                    next: "下一页",
                    previous: "上一页"
                },
            },

            dom: '<"html5buttons"B>lTfgitp',
            buttons: [
                { extend: 'copy', text: "复制" },
                { extend: 'csv', title: options.Title },
                { extend: 'excel', title: options.Title },
                //{
                //    extend: 'pdf', title: options.Title


                //},
                {
                    extend: 'print', text: "打印",
                    customize: function (win) {
                        $(win.document.body).addClass('white-bg');
                        $(win.document.body).css('font-size', '10px');

                        $(win.document.body).find('table')
                                .addClass('compact')
                                .css('font-size', 'inherit');
                    }
                }
            ],
            sAjaxSource: "",
            //如果加上下面这段内容，则使用post方式传递数据
            fnServerData: function (sSource, aoData, fnCallback) {
                var pageIndex = 1;
                var pageSize = 20;
                var sortName = 2;
                var sortOrder = "desc";
                var arrColumn = [];
                for (var i = 0; i < aoData.length; i++) {
                    if (aoData[i].name == "iDisplayStart") {
                        pageIndex = aoData[i].value;
                    }
                    if (aoData[i].name.indexOf("mDataProp") != -1) {
                        arrColumn.push(aoData[i].value);
                    }

                    if (aoData[i].name == "iDisplayLength") {
                        pageSize = aoData[i].value;
                    }
                    if (aoData[i].name == "iSortCol_0") {
                        sortName = aoData[i].value;
                    }
                    if (aoData[i].name == "sSortDir_0") {
                        sortOrder = aoData[i].value;
                    }
                }
                aoData.push({ name: "pageIndex", value: (pageIndex / pageSize + 1) });
                aoData.push({ name: "pageSize", value: pageSize });
                aoData.push({ name: "sortName", value: arrColumn[sortName] });
                aoData.push({ name: "sortOrder", value: sortOrder });
                jForm.Ajax({
                    "url": sSource,
                    "data": aoData,
                    IsNoLoad: true,
                    success: function (result) {
                        fnCallback({
                            recordsFiltered: result.Total,
                            data: result.Rows,
                            recordsTotal: result.Total
                        });
                    }
                });
            },
            fnDrawCallback: function (oSettings) {

                grid.find('.i-checks.grid-check-item').iCheck({
                    checkboxClass: 'icheckbox_square-green',
                    radioClass: 'iradio_square-green'
                });
                grid.find('.i-checks.grid-check-item').on("ifClicked", function (event) {
                    var checked = !$(this).parent().hasClass("checked");
                    if (checked) {
                        $(this).parent().parent().parent().addClass("selected");
                    }
                    else {
                        $(this).parent().parent().parent().removeClass("selected");
                    }
                });
            },
            fnInitComplete: function (oSettings, json) {
                grid.removeAttr("style");
                grid.find('.i-checks.grid-check-all').iCheck({
                    checkboxClass: 'icheckbox_square-green grid-check-all',
                    radioClass: 'iradio_square-green'
                });

                grid.find("input.grid-check-all").on("ifClicked", function (event) {
                    var checked = !$(this).parent().hasClass("checked");
                    grid.find('input.grid-check-all').parent().removeClass("checked");
                    grid.find('input.grid-check-item').parent().removeClass("checked");
                    grid.find('input.grid-check-item').parent().parent().parent().removeClass("selected");
                    if (checked) {
                        grid.find('input.grid-check-all').parent().addClass("checked");

                        grid.find('input.grid-check-item').parent().addClass("checked");
                        grid.find('input.grid-check-item').parent().parent().parent().addClass("selected");
                    }
                });
            }


            //fnServerParams: function (aoData) {
            //    aoData.push(
            //            { "name": "Rule_Name", "value": $("#Rule_Name").val() }
            //        );
            //}

        }, options ? options : {});
        var table = grid.DataTable(p);

        return table;
    },
    //initGrid: function (grid, options) {
    //    var p = $.extend({
    //        method: "POST",
    //        usePager: true,
    //        dataAction: 'server',
    //        checkbox: true,
    //        frozen: false,
    //        frozenDetail: false,
    //        frozenCheckbox: false,
    //        delayLoad: false,
    //        pageSize: 100,
    //        parms: [],
    //        enabledEdit: false,
    //        autoFilter: false,
    //        allowHideColumn: false, //显示切换列按钮
    //        selectRowButtonOnly: false,
    //        toolbarShowInLeft: false,
    //        ShowBtnRefresh: true, //自定义参数
    //        pageSizeOptions: [10, 20, 30, 50, 100, 200, 1000],
    //        pageParmName: "pageIndex",
    //        pagesizeParmName: "pageSize",
    //        sortnameParmName: "sortName",
    //        sortorderParmName: "sortOrder",
    //        columns: [],
    //        rowHeight: 28,                           //行默认的高度
    //        headerRowHeight: 40,                    //表头行的高度
    //        width: 987,
    //        //            width: '99%',
    //        //            height: '99%',
    //        rownumbers: false,
    //        fixedCellHeight: false
    //    }, options ? options : {});
    //    return grid.ligerGrid(p);
    //},


    GridDelete: function (grid, options) {
        var deleteKeys = "";
        var rows = jForm.GetCheckedRows(grid);
        if (rows.length <= 0) {
            jNotify.showError("请选择数据");
            return false;
        }
        $(rows).each(function () {
            if (deleteKeys == "") {
                deleteKeys = this[options.KeyName];
            }
            else {
                deleteKeys += "," + this[options.KeyName];
            }
        });

        if (deleteKeys == null || deleteKeys == "") {
            jNotify.showError("请选择需要删除的行？");
            return false;
        }

        jNotify.Confirm("确认提示", "确定要删除该信息吗？",
          function () {
              jForm.Ajax({
                  url: options.url,
                  data: { deleteKeys: deleteKeys },
                  success: function (data) {
                      jForm.GridSearch(grid);
                  }

              });
          });
    },
    GridAction: function (grid, options) {
        var deleteKeys = "";
        var rows = jForm.GetCheckedRows(grid);
        if (rows.length <= 0) {
            jNotify.showError("请选择需要操作的数据");
            return false;
        }
        $(rows).each(function () {
            if (deleteKeys == "") {
                deleteKeys = this[options.KeyName];
            }
            else {
                deleteKeys += "," + this[options.KeyName];
            }
        });

        if (deleteKeys == null || deleteKeys == "") {
            jNotify.showError("请选择需要操作的行？");
            return false;
        }

        jNotify.Confirm(options.title ? options.title : "信息提示", options.content,
          function () {
              jForm.Ajax({
                  url: options.url,
                  data: { deleteKeys: deleteKeys },
                  success: function (data) {
                      jForm.GridSearch(grid);
                  }

              });
          });
    },
    //查询
    //    Query: function (gridName, formId, options) {

    //        $(formId).find("input,select").each(function () {
    //            grid.setParm($(this).attr("name"), $.trim($(this).val()));
    //        });
    //        grid.options.newPage = 1;
    //        grid.loadData();
    //    },

    GetCheckedRows: function (table) {

        return table.rows(".selected").data();

        //for (var i = 0; i < nTrs.length; i++) {
        //    if ($(nTrs[i]).hasClass('selected')) {
        //        rows.push(table.fnGetData(nTrs[i]));
        //    }
        //}
        //table.find(".grid-check-item.checked").each(function () {
        //    if (deleteKeys == "") {
        //        deleteKeys = $(this).find("input[type='checkbox']").val();
        //    }
        //    else {
        //        deleteKeys += "," + $(this).find("input[type='checkbox']").val();
        //    }
        //});

    },
    GridSearch: function (table) {
        table.draw();
        //$(formId).find("input,select").each(function () {
        //    grid.setParm($(this).attr("name"), $.trim($(this).val()));
        //});
        //grid.options.newPage = 1;
        //grid.loadData();
    }
};
$(function () {
    $('.layout-list-table td:even').css('text-align', 'right');
    $(document).ready(function () {
        $(".layout-edit-from-checkbox>.controls>input[type='checkbox']").iCheck({
            checkboxClass: 'icheckbox_square-green',
            radioClass: 'iradio_square-green'

        });
        $(".layout-edit-from-checkbox>.controls").find("input[type='checkbox']").on("ifClicked", function (event) {

            var checked = !$(this).parent().hasClass("checked");
            if ($(this).attr("valuetype") == "int") {
                $(this).parent().parent().children("input").val((checked ? "1" : "0"));
            }
            else {
                $(this).parent().parent().children("input").val(checked);
            }
        });
    });

});
function Back() {
    window.history.go(-1);
}