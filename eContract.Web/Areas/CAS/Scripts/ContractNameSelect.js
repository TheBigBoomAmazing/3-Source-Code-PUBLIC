function selectContractNameSingle(selectControl, selectedIds) {
    UDcommonSelectSingle(selectControl, selectedIds, "/CAS/Common/ContractNameList");
}

function selectContractNameSingle2(selectControl, selectedIds, selectedValue) {
    UDcommonSelectSingle2(selectControl, selectedIds, selectedValue, "/CAS/Common/ContractNameList");
}


function UDcommonSelectSingle(selectControl, selectedIds, url) {
    selectControl.on("select2:select", function (e) {
        selectedIds.val(e.params.data.id);
    })
    //selectControl.css("width", "100%");
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

function UDcommonSelectSingle2(selectControl, selectedIds, selectedValue, url) {
    //selectControl.select2("val", "");//清空以前选中的值，重新赋值，避免重复
    console.log(selectedValue);
    var dataValue = selectedValue; //$.parseJSON($.parseJSON(selectedValue));
    var str = "";
    for (var i = 0; i < dataValue.length; i++) {
        value = dataValue[i]["id"];
        str = str + value + ",";
    }
    str = str.substring(0, str.length - 1);
    selectedIds.val(str);
    selectControl.on("select2:select", function (e) {
        selectedIds.val(e.params.data.id);
    })
    selectControl.select2({
        placeholder: "Select",
        //allowClear: true,
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

    //selectControl.css("width", "100%");
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