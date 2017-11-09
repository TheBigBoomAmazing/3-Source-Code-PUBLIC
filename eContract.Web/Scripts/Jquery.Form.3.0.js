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
    
    //预加载图片
    prevLoadImage : function (rootpath, paths) {
        for (var i in paths) {
            $('<img />').attr('src', rootpath + paths[i]);
        }
    },
    //显示loading
    showLoading  :  function (message) {
        message = message || "正在处理中...";
        $('body').append("<div class='jloading'>" + message + "</div>");
       
        $.ligerui.win.mask();
    },
    //隐藏loading
    hideLoading  :  function (message) {
        $('body > div.jloading').remove();
        $.ligerui.win.unmask({ id: new Date().getTime() });
    },
    //显示成功提示窗口
    showSuccess: function (message, callback) {
        console.info("234")
        if (typeof (message) == "function" || arguments.length == 0) {
            callback = message;
            message = "操作成功!";
        }
        $.ligerDialog.success(message, "信息提示", callback);
    },
    //显示失败提示窗口
    showError  :  function (message, callback) {
        if (typeof (message) == "function" || arguments.length == 0) {
            callback = message;
            message = "操作失败!";
        }
        $.ligerDialog.error(message, "信息提示", callback);
    },
    //警告
    Warn: function (content, title, fn) {
        $.ligerDialog.alert(content, ((title && title != "") ? title : "系统警告"), fn, "warn");
    },
    //问题描述
    Question: function (content, title, fn) {
        $.ligerDialog.alert(content, ((title && title != "") ? title : "信息提示"), fn, "question");
    },

    Confirm: function (title, content, fn, fn1) {
        $.ligerDialog.confirm(content, ((title && title != "") ? title : "信息确认"), function (r) {
            if (r) {//确定
                if (fn) {
                    fn();
                }
            }
            else {//取消
                if (fn1) {
                    fn1();
                }
            }
        });
    }
    
}

Array.prototype.InArray = function (e) {
    for (i = 0; i < this.length && this[i] != e; i++);
    return !(i == this.length);
}
var arrDialog={};
var jForm = {
    Guid: function () {
        function G() {
            return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1)
        }
        return (G() + G() + "-" + G() + "-" + G() + "-" +
             G() + "-" + G() + G() + G());
    },
    Dialog: function (options) {
        var dialogId = this.Guid();
        var p = $.extend({
            isDrag: true,
            load: false,
            modal: true,
            isResize: false,
            allowClose: true,
            isHidden: false,
            show: true,
            showMax: false,
            showToggle: false,
            showMin: false,
            slide: true,
            width: 600,
            height: 'auto',
            buttons: [
                {
                    text: options.okText ? options.okText : "保存", onclick: function () {

                        // var frm = $(arrDialog[dialogId].jiframe[0].contentWindow.window.document).find("#" + (options.formId ? options.formId : "frmContent"));
                        var win = arrDialog[dialogId].jiframe ? arrDialog[dialogId].jiframe[0].contentWindow : window;
                        options.parentWin = arrDialog[dialogId];
                        if (!options.success) {
                            options.success = function (options, data) {
                                if (options.grid) {
                                    options.grid.loadData();
                                }
                                options.parentWin.close();
                            };
                        }
                        else {
                            options.success(options);
                          
                        }
                        if (arrDialog[dialogId].jiframe) {
                            win.onSubmit(options);
                        }
                       
                    }
                },
                { text: "关闭", onclick: function () { arrDialog[dialogId].close(); } }
            ]
        }, options ? options : {});
        arrDialog[dialogId] = $.ligerDialog.open(p);
        return arrDialog[dialogId];
    },
    // 将/Date()/格式字符串转换成datetime
    DateParse: function (str) {
        var d = str.replace(/^\//, "new ").replace(/\/$/, "");
        d = eval(d);
        return d;
    },
    Validate: function (form, options) {
        var rule = {};
        form.find("input[validate]").each(function () {
            rule[($(this).attr("key") ? $(this).attr("key") : $(this).attr("name"))] = eval("(" + $(this).attr("validate") + ")");

        });

        options = $.extend({
            onKeyup: true,
            onsubmit: true,
            meta: "validate",
            rules: rule,
            ignore: ".ignore",
            errorPlacement: function (lable, element) {
                if (element.hasClass("l-textarea")) {
                    element.addClass("l-textarea-invalid");
                }
                else if (element.hasClass("l-text-field")) {
                    element.parent().addClass("l-text-invalid");
                }
                $(element).removeAttr("title").ligerHideTip();
                $(element).attr("title", lable.html()).ligerTip();
            },
            success: function (lable) {
                var element = $("#" + lable.attr("for"));
                if (element.hasClass("l-textarea")) {
                    element.removeClass("l-textarea-invalid");
                }
                else if (element.hasClass("l-text-field")) {
                    element.parent().removeClass("l-text-invalid");
                }
                $(element).removeAttr("title").ligerHideTip();

            }
        }, options || {});
        return form.validate(options);
    },
    xValidate: function (form, options) {
        try {
            var rule = {};
            //,.radioboxlist[validate],.checkboxlist[checkboxlist]
            form.find("input[validate],textarea[validate],select[validate]").each(function () {
                var name = ($(this).attr("key") ? $(this).attr("key") : $(this).attr("name"));
                rule[name] = eval("(" + $(this).attr("validate") + ")");
            });
            options = $.extend({
                onKeyup: true,
                onfocusout: function (element) {
                    $(element).valid();
                },

                meta: "validate",
                rules: rule,
                ignore: options.ignore ? options.ignore : ".ignore",
                errorPlacement: function (lable, element) {
                    var obj = element.parent().parent();
                    if (element.parent().hasClass("l-text-date")) {
                        obj = obj.parent().parent();
                    }
                    obj.addClass("has-error");
                    obj.find(".help-block").html(lable.html());
                },
                success: function (lable) {
                    var element = $("#" + lable.attr("for"));
                    var obj = element.parent().parent();
                    if (element.parent().hasClass("l-text-date")) {
                        obj = obj.parent().parent();
                    }
                    if (obj.hasClass("has-error")) {
                        obj.removeClass("has-error");
                    }
                    obj.find("p.help-block").html("");
                }

            }, options || {});
            return form.validate(options);
            //if (!vaild) {
            //    return false;
            //}

            //if (options && options.afterCheck) {
            //    options.afterCheck(form, options);
            //    return vaild;
            //}
            return vaild;
        }
        catch (e) {
            jNotify.showError(e);
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
            if ($(this).attr("ltype") == "select") {
                data[fN] = $(this).next().find("input[data-ligerid='" + $(this).attr("id") + "']").val();
            }
            else if ($(this).attr("ltype") == "checkbox") {

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
                if (!options.IsNoLoad) {
                    jNotify.hideLoading();
                }
                if (!result) return;

                if (!result.msgHeader.IsError) {
                    if (options.success) {
                        options.success(result.msgBody, result.msgHeader.Message);
                    }
                }
                else {
                    if (result.msgHeader.StatusCode == -100) {
                        window.location.href = window.location.href;
                    }
                    if (options.error)
                        options.error(result.msgHeader.Message);
                    else {
                        jNotify.showError(result.msgHeader.Message, "");
                    }
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                // jNotify.Error("", XMLHttpRequest.statusCode + ":" + XMLHttpRequest.readyState + ":" + XMLHttpRequest.responseText + ":" + XMLHttpRequest.statusText);
                if (!options.IsNoLoad) {
                    jNotify.hideLoading();
                }
            }

        });
    },

    initGrid: function (grid, options) {
        var p = $.extend({
            method: "POST",
            usePager: true,
            dataAction: 'server',
            checkbox: true,
            frozen: false,
            frozenDetail: false,
            frozenCheckbox: false,
            delayLoad: false,
            pageSize: 20,
            parms: [],
            enabledEdit: false,
            autoFilter: false,
            allowHideColumn: false, //显示切换列按钮
            selectRowButtonOnly: false,
            toolbarShowInLeft: false,
            ShowBtnRefresh: true, //自定义参数
            pageSizeOptions: [10, 20, 30, 50, 100, 200, 1000],
            pageParmName: "pageIndex",
            pagesizeParmName: "pageSize",
            sortnameParmName: "sortName",
            sortorderParmName: "sortOrder",
            columns: [],
            rowHeight: 28,                           //行默认的高度
            headerRowHeight: 40,                    //表头行的高度
            width: 987,
            //            width: '99%',
            //            height: '99%',
            rownumbers: false,
            fixedCellHeight: false
        }, options ? options : {});
        return grid.ligerGrid(p);
    },


    GridDelete: function (grid, options) {
        var rows = grid.getCheckedRows();
        if (rows == null || rows.length <= 0) {
            jNotify.showError("请选择需要删除的行");
            return false;
        }
        jNotify.Confirm("确认提示", "确定要删除该信息吗？",
          function () {
              var deleteKeys = "";
              $(rows).each(function () {
                  if (deleteKeys == "") {
                      deleteKeys = this[options.KeyName];
                  }
                  else {
                      deleteKeys += "," + this[options.KeyName];
                  }
              });
              if (deleteKeys == "") {
                  return false;
              }
              jForm.Ajax({
                  url: options.url,
                  data: { deleteKeys: deleteKeys },
                  success: function (data) {
                      grid.loadData();
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
    GridSearch: function (grid, gridName, formId, options) {

        $(formId).find("input,select").each(function () {
            grid.setParm($(this).attr("name"), $.trim($(this).val()));
        });
        grid.options.newPage = 1;
        grid.loadData();
    }
};
$(function () {
    $('.layout-list-search table td:even').css('text-align', 'right');
});
