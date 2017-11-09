
function selectCitySingle(selectControl, selectedIds,RegionId) {
    commonSelectSingle(selectControl, selectedIds, RegionId, "/CAS/Common/CityList");
}


function selectUser(selectControl, selectedIds, selectedValue) {
    commonSelect(selectControl, selectedIds, selectedValue, "/CAS/DepUserCommon/UserList");
}


function selectUser2(selectControl, selectedIds, selectedValue) {
    commonSelect2(selectControl, selectedIds, selectedValue, "/CAS/DepUserCommon/UserList");
}
function selectCitySingle2(selectControl, selectedIds, selectedValue,RegionId) {
    commonSelectSingle2(selectControl, selectedIds, selectedValue,RegionId, "/CAS/Common/CityList");
}



function commonSelect(selectControl, selectedIds, selectedValue, url) {
    selectControl.on("select2:select", function (e) {
        if (selectedIds.val().length > 0) {
            selectedIds.val(selectedIds.val() + "," + e.params.data.id);
        }
        else {
            selectedIds.val(e.params.data.id);
        }
    })
    selectControl.on("select2:unselect", function (e) {
        selectedIds.val(selectedIds.val().replace("," + e.params.data.id, ''));
        selectedIds.val(selectedIds.val().replace(e.params.data.id + ",", ''));
        selectedIds.val(selectedIds.val().replace(e.params.data.id, ''));
    })

    selectControl.select2({
        placeholder: "Select",
        ajax: {
            url: url,
            dataType: 'json',
            delay: 250,
            data: function (params) {
                return {
                    q: params.term, // search term
                    page: params.page
                };
            },
            processResults: function (data, params) {
                params.page = params.page || 1;

                return {
                    results: data.items,
                    pagination: {
                        more: (params.page * 10) < data.total_count
                    }
                };
            },
            cache: true
        },
        escapeMarkup: function (markup) { return markup; }, // let our custom formatter work
        minimumInputLength: 0,

        templateResult: formatRepoProvince, // omitted for brevity, see the source of this page
        templateSelection: formatRepoProvince,// omitted for brevity, see the source of this page   
        //initSelection: function (element, callback) {  //{# 默认显示option项 #} 
        //    if (selectedValue != null) {
        //        var data = $.parseJSON($.parseJSON(selectedValue));
        //        callback(data);
        //    }
        //}
    });
}

function commonSelectSingle(selectControl, selectedIds, RegionId, url) {
    selectControl.on("select2:select", function (e) {
        selectedIds.val(e.params.data.id);
        var cityCode = e.params.data.id;
        loadSelectRegion(cityCode, RegionId);

    })
    selectControl.select2({
        placeholder: "Select",
        ajax: {
            url: url,
            dataType: 'json',
            delay: 250,
            data: function (params) {
                return {
                    q: params.term, // search term
                    page: params.page
                };
            },
            processResults: function (data, params) {
                params.page = params.page || 1;

                return {
                    results: data.items,
                    pagination: {
                        more: (params.page * 10) < data.total_count
                    }
                };
            },
            cache: true
        },
        escapeMarkup: function (markup) { return markup; }, // let our custom formatter work
        minimumInputLength: 0,

        templateResult: formatRepoProvince, // omitted for brevity, see the source of this page
        templateSelection: formatRepoProvince,// omitted for brevity, see the source of this page   
    });
}

function formatRepoProvince(repo) {
    if (repo.loading) return repo.text;
    var markup = "";
    if (repo.name && repo.name != null && repo.name != 'undefined') {
        markup = "<div>" + "<p hidden='hidden'>" + repo.id + "/" + "</p>" + repo.name + "</div>";
    }
    else {
        markup = "<div>" + "<p hidden='hidden'>" + repo.id + "/" + "</p>" + repo.text + "</div>";
    }
    return markup;
}

function commonSelect2(selectControl, selectedIds, selectedValue, url) {
    //selectContkrol.select2("val", "");//清空以前选中的值，重新赋值，避免重复
    var dataValue = $.parseJSON($.parseJSON(selectedValue));
    var str = "";
    for (var i = 0; i < dataValue.length; i++) {
        value = dataValue[i]["id"];
        str = str + value + ",";
    }
    str = str.substring(0, str.length - 1)
    selectedIds.val(str);
    selectControl.on("select2:select", function (e) {
        if (selectedIds.val().length > 0) {
            selectedIds.val(selectedIds.val() + "," + e.params.data.id);
        }
        else {
            selectedIds.val(e.params.data.id);
        }
    })
    selectControl.on("select2:unselect", function (e) {
        selectedIds.val(selectedIds.val().replace("," + e.params.data.id, ''));
        selectedIds.val(selectedIds.val().replace(e.params.data.id + ",", ''));
        selectedIds.val(selectedIds.val().replace(e.params.data.id, ''));
    })

    selectControl.select2({
        placeholder: "Select",
        ajax: {
            url: url,
            dataType: 'json',
            delay: 250,
            data: function (params) {
                return {
                    q: params.term, // search term
                    page: params.page
                };
            },
            processResults: function (data, params) {
                params.page = params.page || 1;

                return {
                    results: data.items,
                    pagination: {
                        more: (params.page * 10) < data.total_count
                    }
                };
            },
            cache: true
        },
        escapeMarkup: function (markup) { return markup; }, // let our custom formatter work
        minimumInputLength: 0,

        templateResult: formatRepoProvince, // omitted for brevity, see the source of this page
        templateSelection: formatRepoProvince// omitted for brevity, see the source of this page   
        //initSelection: function (element, callback) {  //{# 默认显示option项 #} 
        //    if (selectedValue != null) {
        //        var data = $.parseJSON($.parseJSON(selectedValue));
        //        $(selectControl.val()).each(function () {
        //            data.push({ id: this, text: this });
        //        });
        //        callback(data);
        //    }
        //}
        //dataAdapter: CustomData,
    });

    for (var d = 0; d < dataValue.length; d++) {
        var item = dataValue[d];
        // Create the DOM option that is pre-selected by default
        var option = new Option(item.name, item.id, false, true);
        // Append it to the select
        selectControl.append(option);

        // Update the selected options that are displayed
        selectControl.trigger('change');
    }
}

function commonSelectSingle2(selectControl, selectedIds, selectedValue,RegionId, url) {
    //selectContkrol.select2("val", "");//清空以前选中的值，重新赋值，避免重复
    var dataValue = $.parseJSON($.parseJSON(selectedValue));
    var str = "";
    for (var i = 0; i < dataValue.length; i++) {
        value = dataValue[i]["id"];
        str = str + value + ",";
    }
    str = str.substring(0, str.length - 1)
    selectedIds.val(str);
    selectControl.on("select2:select", function (e) {
        selectedIds.val(e.params.data.id);
        var cityCode = e.params.data.id;
        loadSelectRegion(cityCode, RegionId);
    })
    selectControl.select2({
        placeholder: "Select",
        ajax: {
            url: url,
            dataType: 'json',
            delay: 250,
            data: function (params) {
                return {
                    q: params.term, // search term
                    page: params.page
                };
            },
            processResults: function (data, params) {
                params.page = params.page || 1;

                return {
                    results: data.items,
                    pagination: {
                        more: (params.page * 10) < data.total_count
                    }
                };
            },
            cache: true
        },
        escapeMarkup: function (markup) { return markup; }, // let our custom formatter work
        minimumInputLength: 0,

        templateResult: formatRepoProvince, // omitted for brevity, see the source of this page
        templateSelection: formatRepoProvince// omitted for brevity, see the source of this page   
        //initSelection: function (element, callback) {  //{# 默认显示option项 #} 
        //    if (selectedValue != null) {
        //        var data = $.parseJSON($.parseJSON(selectedValue));
        //        $(selectControl.val()).each(function () {
        //            data.push({ id: this, text: this });
        //        });
        //        callback(data);
        //    }
        //}
        //dataAdapter: CustomData,
    });

    for (var d = 0; d < dataValue.length; d++) {
        var item = dataValue[d];
        // Create the DOM option that is pre-selected by default
        var option = new Option(item.name, item.id, false, true);
        // Append it to the select
        selectControl.append(option);

        // Update the selected options that are displayed
        selectControl.trigger('change');
    }
}

//查询当前用户所在的城市
function loadSelectRegion(cityCodeValue, RegionId) {
    $.ajax({
        url: '/CAS/Common/loadSelectRegionValue',
        async: true,
        data: {
            cityCode: cityCodeValue
        },
        timeout: 5000,    //超时时间
        dataType: 'text',    //返回的数据格式：json/xml/html/script/jsonp/text
        success: function (result) {
            //selectCitySingle2($("#sel_menuCity"), $("#selected_City"), data);
            RegionId.val(result);
        }
    });
};
