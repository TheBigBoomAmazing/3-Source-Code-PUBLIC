$(function () {
    $("#modelLogin").find(".btn-primary").on("click", function () {
        var UserAccount = $(".login-account").val();
        var password = $(".login-pwd").val();
        $.ajax({
            url: "/Account/Login",
            type: "post",
            data: { UserAccount: UserAccount, password: password, isAjax :true},
            success: function (json) {
                if (!json.msgHeader.IsError) {
                    $(".login-error").hide();
                    location.reload();
                } else {
                    $(".login-error").find("dd").text(json.msgHeader.Message);
                    $(".login-error").show();
                }
            }
        });
        return false;
    });

    //关键字搜索
    searchEvent("top-search", "search-result", "searchbtn");
    //searchEvent("lay-key", "lay-search-result", "lay-searchbtn");
    $('body').click(function () {
        $('#search-result').hide();
        //$('#lay-search-result').hide();
    });
})

function searchEvent(inputId, listId, btnId) {
    inputEvent(inputId, listId);
    //btnEvent(inputId, btnId);
    keyboardEvent(inputId, listId);
    searchResultListEvent(inputId, listId);
}

function inputEvent(inputId, listId) {
    $("#" + inputId).click(function () {
        var keyword = $.trim($('#' + inputId).val());
        if (keyword.length > 0) {
            $.ajax({
                type: "POST",
                dataType: "json",
                timeout: 0,
                url: "/Product/GetSearchKeysResult",
                data: { Key: keyword },
                success: function (data) {
                    var s = "";
                    if (data.RESULT.length > 0) {
                        for (var i = 0; i < data.RESULT.length; i++) {
                            s += "<li class='resultItem' productCode='" + data.RESULT[i].productCode + "'>" + data.RESULT[i].productName + "</li>";
                        }
                    }
                    $('#' + listId + ' ul').html(s);
                    $('#' + listId).show();
                },
                error: function () {
                }
            });
        } else {
            $('#' + listId).hide();
        }
    });
}

function btnEvent(inputId, btnId) {
    $('#' + btnId).click(function () {
        var keyword = $('#' + inputId).val();
        $.ajax({
            type: "POST",
            dataType: "text",
            timeout: 0,
            url: "/Home/SaveSearchKeyword",
            data: { Keyword: keyword },
            success: function () {

            }
        });
        location.href = "/Home/ProductList?keyword=" + encodeURI(keyword);
        return false;
    });
}

function keyboardEvent(inputId, listId) {
    //if ($("#" + inputId).focus()) {
    $(document).keyup(function(event) {
        var keyword = $.trim($('#' + inputId).val());

        if (event.keyCode == 13) {
            //回车键搜索,如果有选中项，则直接跳到详情页
            var isSelected = 0;
            var productCode = 0;
            $('#' + listId + ' ul li').each(function () {
                if ($(this).hasClass('resultItemSelect')) {
                    isSelected = 1;
                    productCode = $(this).attr('productCode');
                }
            });
            if (isSelected == 1) {
                location.href = $.format(JsDetailFormatUrl, encodeURI(productCode)); 
            } else {
                if (keyword.length > 0) {
                    location.href = "/Home/ProductList?keyword=" + encodeURI(keyword);
                }
            }
        } else if (event.keyCode == 38 || event.keyCode == 40) {
            //方向键选择结果。
            var eventCode = event.keyCode;
            //获取当前选中的索引
            var curIndex = -1;
            var maxIndex = 0;
            var minIndex = 0;
            $('#' + listId + ' ul li').each(function (i) {
                if ($(this).hasClass('resultItemSelect')) {
                    $(this).removeClass('resultItemSelect');
                    curIndex = i;
                }
                maxIndex = i;
            });
            if (eventCode == 38) {
                if (curIndex == -1 || curIndex == 0) {
                    $('#' + listId + ' ul li').each(function (i) {
                        if (i == maxIndex) {
                            $(this).addClass('resultItemSelect');
                            $("#" + inputId).val($(this).text());
                        }
                    });
                } else {
                    curIndex = curIndex - 1;
                    $('#' + listId + ' ul li').each(function (i) {
                        if (i == curIndex) {
                            $(this).addClass('resultItemSelect');
                            $("#" + inputId).val($(this).text());
                        }
                    });
                }
            } else {
                if (curIndex == maxIndex) {
                    $('#' + listId + ' ul li').each(function (i) {
                        if (i == 0) {
                            $(this).addClass('resultItemSelect');
                            $("#" + inputId).val($(this).text());
                        }
                    });
                } else {
                    curIndex = curIndex + 1;
                    $('#' + listId + ' ul li').each(function (i) {
                        if (i == curIndex) {
                            $(this).addClass('resultItemSelect');
                            $("#" + inputId).val($(this).text());
                        }
                    });
                }
            }
        } else {
            //非回车键弹出下垃提示
            if (keyword.length > 0) {
                $.ajax({
                    type: "POST",
                    dataType: "json",
                    timeout: 0,
                    url: "/Home/GetSearchKeysResult",
                    data: { Key: keyword },
                    success: function(data) {
                        var s = "";
                        if (data.RESULT.length > 0) {
                            for (var i = 0; i < data.RESULT.length; i++) {
                                s += "<li class='resultItem' productCode='" + data.RESULT[i].productCode + "'>" + data.RESULT[i].productName + "</li>";
                            }
                        }
                        $('#' + listId + ' ul').html(s);
                        $('#' + listId).show();
                    },
                    error: function() {
                    }
                });
            } else {
                $('#' + listId).hide();
            }
        }
    });
    //}
}

function searchResultListEvent(inputId,listId) {
    $('#' + listId+' li').on('click', function () {
        var s = $(this).text();
        var productCode = $(this).attr('productCode');
        $("#" + inputId).val(s);
        $('#' + listId).hide();
        location.href = $.format(JsDetailFormatUrl, encodeURI(productCode)); 
    });
}
