function HashTable() {
    var size = 0;
    var entry = new Object();
    this.add = function (key, value) {
        if (!this.containsKey(key)) {
            size++;
        }
        entry[key] = value;
    }

    this.getValue = function (key) {
        return this.containsKey(key) ? entry[key] : null;
    }

    this.remove = function (key) {
        if (this.containsKey(key) && (delete entry[key])) {
            size--;
        }
    }

    this.containsKey = function (key) {
        return (key in entry);
    }

    this.containsValue = function (value) {
        for (var prop in entry) {
            if (entry[prop] == value) {
                return true;
            }
        }
        return false;
    }

    this.getValues = function () {
        var values = new Array();
        for (var prop in entry) {
            values.push(entry[prop]);
        }
        return values;
    }

    this.getKeys = function () {
        var keys = new Array();
        for (var prop in entry) {
            keys.push(prop);
        }
        return keys;
    }

    this.getSize = function () {
        return size;
    }

    this.clear = function () {
        size = 0;
        entry = new Object();
    }
}

(function ($) {
    $.UploadHelper = {
        //hiddenArea: "",
        fileIds: new Array(),
        DelUploaderFile: function (obj, hiddenId, fileIdArray) {
            fileIdArray = $("#" + hiddenId).val().split(',');
            jNotify.Confirm("",
                "Please confirm to delete the file.",
                function () {
                    $.ajax({
                        type: "Get",
                        url: "/Common/DeleteUploaderFile",
                        data: { id: $(obj).attr("data") },
                        dataType: "json",
                        success: function (json1) {
                            if (!json1.msgHeader.IsError) {
                                $(obj).parent().remove();
                                var index = $.inArray($(obj).attr("data"), fileIdArray);//$.UploadHelper.fileIds
                                if (index > -1) {
                                    fileIdArray.splice(index, 1)
                                }
                                $("#" + hiddenId).val(fileIdArray.join(','));
                            } else {
                                jNotify.showError("Delete failed");
                            }
                        }, error: function (json2) {
                            jNotify.showError("Delete failed");
                        }
                    });
                });

        },
        init: function (btnId, hiddenId, myFiles, readonly,PDFTYPE) {
            //隐藏域
            if (typeof (hiddenId) != typeof (undefined)) {
                //$.UploadHelper.hiddenArea = hiddenId;
            }
            //创建文件容器
            $("#" + btnId).after('<ul id="filesUl-' + hiddenId + '" style="list-style: none;padding-left: 0;"></ul>');

            //显示已上传的附件
            if (typeof (myFiles) != typeof (undefined)) {
                $.ajax({
                    url: "/Common/GetFilesDemo",
                    method: "GET",
                    type: "json",
                    data: { ids: myFiles },
                    success: function (result) {
                        var dbFileIds = $("#" + hiddenId).val().split(',');
                        var dbFiles = eval('(' + result + ')');
                        for (var index = 0; index < dbFiles.length; index++) {
                            html = '<li id="file-' +
                           dbFiles[index].AttachmentId +
                           '" class="localFile " style="padding: 5px; width: 99%; float: left; margin: 1px 0 2px 0;border-bottom: 1px solid #d0d0d0;">' +
                           '<div style="float: left; line-height: 24px;" id="div-' +
                           dbFiles[index].AttachmentId +
                           '">' +
                           '<label >' +
                           dbFiles[index].FileName +
                           '</label></div>';
                            if (readonly) {
                                html = html +
                            '<input type="button" class="delUploaderFile" disabled="disabled" style="float: right;" data="'
                            }
                            else {
                                html = html +
                            '<input type="button" class="delUploaderFile" style="float: right;" data="'
                            }
                            html = html + dbFiles[index].AttachmentId +
                           '" value="Delete"><p class="progress" style="height:0;margin:0;"></p></li>';
                            $("#filesUl-" + hiddenId).append(html);
                            $("#file-" + dbFiles[index].AttachmentId).find("input").on("click", function () {
                                $.UploadHelper.DelUploaderFile(this, hiddenId, dbFileIds);
                            });
                        }

                    },
                    error: function (result) {
                        console.info(result)
                    }
                });
            }
            if (PDFTYPE) {
                uploader = new plupload.Uploader({
                    browse_button: btnId, //触发文件选择对话框的按钮，为那个元素id
                    url: "/Common/UploadFiles", //服务器端的上传页面地址
                    multi_selection: true, //是否可以多选
                    unique_names: true, //生成唯一文件名
                    filters: {
                        mime_types: [//只允许上传pdf
                            { title: "PDF files", extensions: "pdf" }
                        ],
                        max_file_size: '20Mb', //最大只能上传20MB的文件
                        prevent_duplicates: true //不允许选取重复文件
                    }
                });
            }
            else {
                uploader = new plupload.Uploader({
                    browse_button: btnId, //触发文件选择对话框的按钮，为那个元素id
                    url: "/Common/UploadFiles", //服务器端的上传页面地址
                    multi_selection: true, //是否可以多选
                    unique_names: true, //生成唯一文件名
                    filters: {
                        mime_types: [//只允许上传pdf和word文档
                            { title: "DOC files", extensions: "doc,docx" },
                            { title: "PDF files", extensions: "pdf" }
                        ],
                        max_file_size: '20Mb', //最大只能上传20MB的文件
                        prevent_duplicates: true //不允许选取重复文件
                    }
                });
            }
            //实例化一个plupload上传对象


            //在实例对象上调用init()方法进行初始化
            uploader.init();

            //绑定新增文件事件：更新UI,赋值
            //uploader为当前的plupload实例对象
            //files为一个数组，里面的元素为本次添加到上传队列里的文件对象
            uploader.bind('FilesAdded',
                function (uploader, files) {
                    var html = "";
                    var dbFileIds = $("#"+hiddenId).val().split(',');
                    for (var i = 0; i < files.length; i++) {
                        var orginalName = files[i].name;
                        var extension = orginalName.substr(orginalName.indexOf('.'));
                        var fileName = files[i].id + extension;
                        html = '<li id="file-' +
                            files[i].id +
                            '" class="localFile " style="padding: 5px; width: 99%; float: left; margin: 1px 0 2px 0;border-bottom: 1px solid #d0d0d0;">' +
                            '<div style="float: left; line-height: 24px;" id="div-' +
                            files[i].id +
                            '">' +
                            '<label >' +
                            files[i].name +
                            '</label></div>' +
                            '<input type="button" class="delUploaderFile" style="float: right;" ' +
                            'value="Delete"><p class="progress" style="height:5px;margin:0;background-color: green;display: block;"></p></li>';
                        $("#filesUl-" + hiddenId).append(html);
                        $("#file-" + files[i].id).find("input").on("click", function () {
                            $.UploadHelper.DelUploaderFile(this, hiddenId, dbFileIds);
                        });
                    }
                    uploader.start(); //开始上传

                });

            //绑定上传文件事件：显示进度条
            //uploader为当前的plupload实例对象
            //file为触发此事件的文件对象
            uploader.bind('UploadProgress',
                function (uploader, file) {
                    $('#file-' + file.id + ' .progress').css('width', (file.percent - 5) + '%'); //控制进度条
                });

            //绑定上传文件事件：上传文件完成
            //uploader为当前的plupload实例对象
            //file为触发此事件的文件对象
            //responseObject为服务器返回的信息对象
            uploader.bind('FileUploaded',
                function (uploader, file, responseObject) {
                    var response = eval("(" + JSON.parse(JSON.stringify(responseObject.response)) + ")");
                    if (response.msgHeader.IsError) {
                        var html = '<label style="margin-left: 10px;color: red;display: block;">Upload failed：' +
                            response.msgHeader.Message +
                            '</label>';
                        $('#div-' + file.id).append(html); //显示失败信息
                        $('#file-' + file.id + ' .progress').css('width', '0'); //控制进度条0
                    } else {
                        $('#file-' + file.id + ' .progress').css('width', '100%'); //控制进度条100%
                        //$.UploadHelper.files.add(file.target_name, response.msgHeader.Message);
                        $('#file-' + file.id).find("input").attr("data", response.msgHeader.Message);
                        var dbFileId = response.msgHeader.Message;
                        //$.UploadHelper.fileIds.push(response.msgHeader.Message);
                        $("#" + hiddenId).val($("#" + hiddenId).val() + "," + dbFileId);//$.UploadHelper.fileIds.join(',')
                    }
                });

            //绑定上传文件事件：上传失败
            //uploader为当前的plupload实例对象
            //errObject为错误对象
            uploader.bind('Error',
                function (uploader, errObject) {
                    if (errObject.code == "-600") {
                        jNotify.showError("Upload document is exceeding limit, maxium is 20M.");
                    }
                });
        }
    }
})(jQuery);