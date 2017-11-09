(function ($) {
    $.ContractType = {
        contractTypeList: {},//当前用户所有可申请的合同类型
        currentContractTypeEntity: null,//当前页面显示的合同类型

        //获取合同类型
        getContractTypeList: function () {
            var contractTypeListUrl = "/ApplayContract/GetContractTypeList";
            if ($("#hidEditType").val() == "ContractType") {
                contractTypeListUrl = "/ApplayContract/GetAllSubmitContractTypeList";
            }
            console.log(contractTypeListUrl);
            $.ajax({
                url: contractTypeListUrl,
                method: "get",
                dataType: "json",
                async: false,
                data: {},
                success: function (result) {
                    $.ContractType.contractTypeList = eval("(" + result + ")");
                        var contractFroupHtml = '<option value="">===Select===</option>';
                        $("#CONTRACT_TYPE_NAME").empty();
                        for (var i = 0; i < $.ContractType.contractTypeList.length; i++) {
                            var item = $.ContractType.contractTypeList[i];
                            contractFroupHtml += '<option value="' + item.ContractTypeName + '">' + item.ContractTypeName + '</option>';
                        }
                        $("#CONTRACT_TYPE_NAME").append(contractFroupHtml);

                }, error: function (res) {
                    jNotify.showError(res);
                }
            });
        },

        //初始化
        initData: function () {
            $.ContractType.getContractTypeList();
        }
    }

    $.ContractType.initData();
})(jQuery);