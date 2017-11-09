(function ($) {
    $.ApplayContract = {
        contractTypeList: {},//当前用户所有可申请的合同类型
        currentContractTypeEntity: null,//当前页面显示的合同类型

        //获取合同类型，并绑定费列罗方的change事件
        getContractTypeList: function () {
            var contractTypeListUrl = "/ApplayContract/GetContractTypeList";
            if ($("#hidEditType").val() == "View") {
                contractTypeListUrl = "/ApplayContract/GetAllContractTypeList";
            }
            $.ajax({
                url: contractTypeListUrl,
                method: "get",
                dataType: "json",
                async: false,
                data: {},
                success: function (result) {
                    debugger
                    $.ApplayContract.contractTypeList = eval("(" + result + ")");
                    $("#FerreroEntity").change(function () {
                        var contractFroupHtml = '<option value="">===Select===</option>';
                        $("#ContractTypeId").empty();
                        for (var i = 0; i < $.ApplayContract.contractTypeList.length; i++) {
                            var item = $.ApplayContract.contractTypeList[i];
                            var ferreroEntity = $("#FerreroEntity").val();
                            if (ferreroEntity != "") {
                                if (ferreroEntity == item.FerreroEntity) {
                                    contractFroupHtml += '<option value="' + item.ContractTypeId + '">' + item.ContractTypeName + '</option>';
                                }
                            }
                        }
                        $("#ContractTypeId").append(contractFroupHtml);
                    });
                }, error: function (res) {
                    jNotify.showError(res);
                }
            });
        },

        //绑定合同类型的change事件
        contractTypeChange: function () {
            var selContractTypeId = $("#ContractTypeId").val();
            for (var i = 0; i < $.ApplayContract.contractTypeList.length; i++) {
                if (selContractTypeId == $.ApplayContract.contractTypeList[i].ContractTypeId) {
                    $.ApplayContract.currentContractTypeEntity = $.ApplayContract.contractTypeList[i];
                    break;
                }
            }
            if ($.ApplayContract.currentContractTypeEntity == null) {
                jNotify.showError("The contract type is exception, please refresh the page.");
                return;
            }
            //控制是否显示模板页
            if ($.ApplayContract.currentContractTypeEntity.IsTemplateContract) {
                $("#templateContract").css("display", "block");
                $("#normalContract").css("display", "none");
            } else {
                $("#templateContract").css("display", "none");
                $("#normalContract").css("display", "block");
            }
            //控制页面哪些字段显示
            var attrArray = [$.ApplayContract.currentContractTypeEntity.CounterpartyEn
                , $.ApplayContract.currentContractTypeEntity.CounterpartyCn
                , $.ApplayContract.currentContractTypeEntity.ContractName
                , $.ApplayContract.currentContractTypeEntity.ContractTerm
                , $.ApplayContract.currentContractTypeEntity.ContractOwner//add
                , $.ApplayContract.currentContractTypeEntity.ContractInitiator//add
                , $.ApplayContract.currentContractTypeEntity.ApplyDate//add
                , $.ApplayContract.currentContractTypeEntity.Capex
                , $.ApplayContract.currentContractTypeEntity.IsMasterAgreement
                , $.ApplayContract.currentContractTypeEntity.Supplementary
                , $.ApplayContract.currentContractTypeEntity.BudgetType
                , $.ApplayContract.currentContractTypeEntity.TemplateNo
                //, $.ApplayContract.currentContractTypeEntity.ContractPrice
                //, $.ApplayContract.currentContractTypeEntity.EstimatedPrice
                , $.ApplayContract.currentContractTypeEntity.Currency
                , $.ApplayContract.currentContractTypeEntity.PrepaymentAmount
                , $.ApplayContract.currentContractTypeEntity.PrepaymentPercentage
                , $.ApplayContract.currentContractTypeEntity.ContractKeyPoints
                , $.ApplayContract.currentContractTypeEntity.TemplateNo
                , $.ApplayContract.currentContractTypeEntity.TemplateName
                , $.ApplayContract.currentContractTypeEntity.TemplateTerm
                , $.ApplayContract.currentContractTypeEntity.TemplateOwner//add
                , $.ApplayContract.currentContractTypeEntity.TemplateInitiator//add
                , $.ApplayContract.currentContractTypeEntity.ScopeOfApplication
                , $.ApplayContract.currentContractTypeEntity.HasAttachment
                , $.ApplayContract.currentContractTypeEntity.BudgetAmount
                , $.ApplayContract.currentContractTypeEntity.InternalORInvestmentOrder
            ];
            var elemetContentIdArray = ["#div_CounterpartyEn"
                , "#div_CounterpartyCn"
                , "#div_ContractName"
                , "#div_ContractTerm"
                , "#div_ContractOwner"
                , "#div_ContractInitiator"
                , "#div_ApplyDate"
                , "#div_Capex"
                , "#div_IsMasterAgreement"
                , "#div_Supplementary"
                , "#div_BudgetType"
                , "#div_TemplateNo"//?
                //, "#div_ContractPrice"
                //, "#div_EstimatedPrice"
                , "#div_Currency"
                , "#div_PrepaymentAmount"
                , "#div_PrepaymentPercentage"
                , "#div_ContractKeyPoints"
                , "#div_TemplateNo"//?
                , "#div_TemplateName"
                , "#div_TemplateTerm"
                , "#div_TemplateOwner"
                , "#div_TemplateInitiator"
                , "#div_ScopeOfApplication"
                , "#div_ContractAttachment"
                , "#div_BudgetAmount"
                , "#div_InternalORInvestmentOrder"
            ];
            $.ApplayContract.isShowElement(attrArray, elemetContentIdArray, $.ApplayContract.currentContractTypeEntity.IsTemplateContract);

        },

        //私有方法：控制页面元素显示
        isShowElement: function (attrArray, elemetContentIdArray, isTemplate) {
            for (var i = 0; i < attrArray.length; i++) {
                if (attrArray[i]) {
                    //单行显示处理
                    if (elemetContentIdArray[i] == "#div_ContractTerm"
                        || elemetContentIdArray[i] == "#div_ContractKeyPoints"
                        || elemetContentIdArray[i] == "#div_TemplateTerm"
                        || elemetContentIdArray[i] == "#div_ScopeOfApplication"
                        ) {
                        //$(elemetContentIdArray[i]).css("display", "flex");
                    } else {
                        $(elemetContentIdArray[i]).css("display", "inline-block");
                    }

                    if (isTemplate) {
                        $("#templateContract").find(elemetContentIdArray[i]).find("select,input,textarea").removeAttr("disabled");
                        $("#normalContract").find(elemetContentIdArray[i]).find("select,input,textarea").attr("disabled", "disabled");
                    } else {
                        $("#normalContract").find(elemetContentIdArray[i]).find("select,input,textarea").removeAttr("disabled");
                        $("#templateContract").find(elemetContentIdArray[i]).find("select,input,textarea").attr("disabled", "disabled");
                    }
                }
                else {
                    $(elemetContentIdArray[i]).css("display", "none");
                    $("#normalContract").find(elemetContentIdArray[i]).find("select,input,textarea").attr("disabled", "disabled");
                    $("#templateContract").find(elemetContentIdArray[i]).find("select,input,textarea").attr("disabled", "disabled");
                }
            }

            //样式调整
            var normalItems = $("#normalContract").children(".form-group");
            for (var itemIndex = 0; itemIndex < normalItems.length; itemIndex++) {
                if ($($(normalItems[itemIndex]).children()[0]).css("display") == "none") {
                    $($(normalItems[itemIndex]).children()[1]).removeClass("width-6").addClass("width-12");
                    $($(normalItems[itemIndex]).children()[1]).find(".control-label").removeClass("col-sm-6").addClass("col-sm-3");//标题
                    $($(normalItems[itemIndex]).children()[1]).find(".controls").removeClass("col-sm-6").addClass("col-sm-9");//内容
                }
                if ($($(normalItems[itemIndex]).children()[1]).css("display") == "none") {
                    $($(normalItems[itemIndex]).children()[0]).removeClass("width-6").addClass("width-12");
                    $($(normalItems[itemIndex]).children()[0]).find(".control-label").removeClass("col-sm-6").addClass("col-sm-3");//标题
                    $($(normalItems[itemIndex]).children()[0]).find(".controls").removeClass("col-sm-6").addClass("col-sm-9");//内容
                }
                if (($($(normalItems[itemIndex]).children()[0]).css("display") == "block"
                    || $($(normalItems[itemIndex]).children()[0]).css("display") == "inline-block")
                    && ($($(normalItems[itemIndex]).children()[1]).css("display") == "block"
                    || $($(normalItems[itemIndex]).children()[1]).css("display") == "inline-block")) {
                    for (var i = 0; i < 2; i++) {
                        $($(normalItems[itemIndex]).children()[i]).removeClass("width-12").addClass("width-6");
                        $($(normalItems[itemIndex]).children()[i]).find(".control-label").removeClass("col-sm-3").addClass("col-sm-6");//标题
                        $($(normalItems[itemIndex]).children()[i]).find(".controls").removeClass("col-sm-9").addClass("col-sm-6");//内容
                    }
                }
            }
            var tempItems = $("#templateContract").children(".form-group");
            for (var itemIndex = 0; itemIndex < tempItems.length; itemIndex++) {
                if ($($(tempItems[itemIndex]).children()[0]).css("display") == "none") {
                    $($(tempItems[itemIndex]).children()[1]).removeClass("width-6").addClass("width-12");
                    $($(tempItems[itemIndex]).children()[1]).find(".control-label").removeClass("col-sm-6").addClass("col-sm-3");//标题
                    $($(tempItems[itemIndex]).children()[1]).find(".controls").removeClass("col-sm-6").addClass("col-sm-9");//内容
                }
                if ($($(tempItems[itemIndex]).children()[1]).css("display") == "none") {
                    $($(tempItems[itemIndex]).children()[0]).find(".control-label").removeClass("col-sm-6").addClass("col-sm-3");//标题
                    $($(tempItems[itemIndex]).children()[0]).find(".controls").removeClass("col-sm-6").addClass("col-sm-9");//内容
                }
                if (($($(tempItems[itemIndex]).children()[0]).css("display") == "block"
                    || $($(tempItems[itemIndex]).children()[0]).css("display") == "inline-block")
                    && ($($(tempItems[itemIndex]).children()[1]).css("display") == "block"
                    || $($(tempItems[itemIndex]).children()[1]).css("display") == "inline-block")) {
                    for (var i = 0; i < 2; i++) {
                        $($(tempItems[itemIndex]).children()[i]).removeClass("width-12").addClass("width-6");
                        $($(tempItems[itemIndex]).children()[i]).find(".control-label").removeClass("col-sm-3").addClass("col-sm-6");//标题
                        $($(tempItems[itemIndex]).children()[i]).find(".controls").removeClass("col-sm-9").addClass("col-sm-6");//内容

                    }
                }
            }
        },

        //私有方法：save控制属性变更
        changeElement: function () {
            var btnObj = $("#btnSubmit");
            if (typeof (btnObj) != typeof (undefined)) {
                $("#btnSubmit").removeAttr("disabled");
            }
        },
        //保存
        save: function (type) {
            debugger
            var nameValue = false;
            if ($.ApplayContract.currentContractTypeEntity.ContractName && $("#selected_ContractName").val() == "") {
                jNotify.showError("Please select Contract Name !");
                nameValue = false;
                return nameValue;
            }
            var valid = $("#applayForm").valid();
            if (valid) {
                //合同原件是否上传校验
                if ($.ApplayContract.currentContractTypeEntity.HasAttachment && $("#fileIds").val() == "") {
                    jNotify.showError("Please upload the original contract.");
                    $.ApplayContract.changeElement();
                    return;
                }
                ////如果是补充合同，校验
                //if ($.ApplayContract.currentContractTypeEntity.Supplementary) {
                //    //没有上传原合同文件 且 没有选择原合同
                //    if ($("#selOriginalContract").val() == "" && $("#OriginalFileIds").val() == "") {
                //        jNotify.showError("请上传/选择原合同文件！");
                //        return;
                //    }
                //    if ($("#selOriginalContract").val() != "" && $("#OriginalFileIds").val() != "") {
                //        jNotify.showError("上传原合同文件，选择原合同，二者只能选择一项进行！");
                //        return;
                //    }
                //}

                //如果是补充合同，校验
                var checked = $("[name='Supplementary']").val();
                if (checked == "true") {
                    if ($("#selOriginalContract").val() == "" && $("#OriginalFileIds").val() == "") {
                        jNotify.showError("Please upload/choose the document.");
                        $.ApplayContract.changeElement();
                        return;
                    }
                    if ($("#selOriginalContract").val() != "" && $("#OriginalFileIds").val() != "") {
                        jNotify.showError("Please upload or choose the contract.");
                        $.ApplayContract.changeElement();
                        return;
                    }
                }
                //是否需要领导批注且申请用户是否有领导
                if (type == "2") {
                    var outresult;
                    $.ajax({
                        type: "Get",
                        async: false,
                        url: "/ApplayContract/CheckLeaderExist",
                        data: { contractTypeId: $("#ContractTypeId").val() },
                        dataType: "json",
                        success: function (result) {
                            console.info(result)
                            if (!result.msgHeader.IsError) {
                                outresult = true;
                            } else {
                                jNotify.showError("ERROR: LM. Please contact system administrator.");
                                outresult = false;
                            }
                        }
                    });
                    if (outresult) {

                    } else {
                        $.ApplayContract.changeElement();
                        return outresult;
                    }
                }
                //驳回重新提交的合同需要填写补充说明
                var remarkValue = $("#explanation").val();
                var contractStatus = $("#ContractStatus").val();
                if (contractStatus == "4" && type == "2" && remarkValue == "") {
                    //$('#event-modal').modal();                   
                    if (remarkValue == "") {
                        jNotify.showError("Summary of revision is required.")
                    }
                    $.ApplayContract.changeElement();
                    return false;
                }
                //申请人是否有部门总监
                if (type == "2") {
                    var outresult;
                    $.ajax({
                        type: "Get",
                        async: false,
                        url: "/ApplayContract/CheckDEPManagerExist",
                        data: { contractTypeId: $("#ContractTypeId").val() },
                        dataType: "json",
                        success: function (result) {
                            console.info(result)
                            if (!result.msgHeader.IsError) {
                                outresult = true;
                            } else {
                                jNotify.showError("ERROR: HD. Please contact system administrator.");
                                outresult = false;
                            }
                        }
                    });
                    if (outresult) {

                    } else {
                        $.ApplayContract.changeElement();
                        return outresult;
                    }

                }

                //申请人是否有大区总监
                if (type == "2") {
                    var outresult;
                    $.ajax({
                        type: "Get",
                        async: false,
                        url: "/ApplayContract/CheckRegionManagerExist",
                        data: { contractTypeId: $("#ContractTypeId").val() },
                        dataType: "json",
                        success: function (result) {
                            console.info(result)
                            if (!result.msgHeader.IsError) {
                                outresult = true;
                            } else {
                                jNotify.showError("ERROR: RSM. Please contact system administrator.");
                                outresult = false;
                            }
                        }
                    });
                    if (outresult) {

                    } else {
                        $.ApplayContract.changeElement();
                        return outresult;
                    }

                }
                //申请人是否有用户领导
                if (type == "2") {
                    var outresult;
                    $.ajax({
                        type: "Get",
                        async: false,
                        url: "/ApplayContract/CheckLineManagerExist",
                        data: { contractTypeId: $("#ContractTypeId").val() },
                        dataType: "json",
                        success: function (result) {
                            console.info(result)
                            if (!result.msgHeader.IsError) {
                                outresult = true;
                            } else {
                                jNotify.showError("ERROR:No LM. Please contact system administrator.");
                                outresult = false;
                            }
                        }
                    });
                    if (outresult) {

                    } else {
                        $.ApplayContract.changeElement();
                        return outresult;
                    }
                }

                //校验模板合同的模板编号是否重复 $("#TemplateNoForInput").val()
                if ($.ApplayContract.currentContractTypeEntity.IsTemplateContract) {
                    var outresult;
                    $.ajax({
                        type: "Get",
                        async: false,
                        url: "/ApplayContract/CheckTemplateNo",
                        data: { templateNo: $("#TemplateNo").val(), contractID: $("#ContractId").val() },
                        dataType: "json",
                        success: function (result) {
                            console.info(result)
                            $("#uploadLog").hide();
                            if (!result.msgHeader.IsError) {
                                outresult = true;
                            } else {
                                jNotify.showError("This template number is existing, please re-input.");
                                outresult = false;
                            }
                        }
                    });
                    if (outresult) {

                    } else {
                        $.ApplayContract.changeElement();
                        return outresult;
                    }
                }

                var privalue = false;
                if ($("#IsMasterAgreement").val() == "true") {
                    if ($("#TaxEST").val() == "" || $("#TaxEST").val() == null) {
                        jNotify.showError("Tax is required!");
                        privalue = false;
                    } else {
                        $("#Tax").val($("#TaxEST").val());
                        $("#ContractPrice").val();
                        privalue = true;
                    }
                    if ($("#EstimatedPrice").val() == "") {
                        jNotify.showError("Estimated Price is NULL");
                        privalue = false;
                    }
                    if (privalue) {

                    } else {
                        $.ApplayContract.changeElement();
                        return privalue;
                    }
                }
                if ($("#IsMasterAgreement").val() == "false") {
                    if ($("#TaxCON").val() == "" || $("#TaxCON").val() == null) {
                        jNotify.showError("Tax is required!");
                        privalue = false;
                    } else {
                        $("#Tax").val($("#TaxCON").val());
                        $("#EstimatedPrice").val();
                        privalue = true;
                    }
                    if ($("#ContractPrice").val() == "") {
                        jNotify.showError("Contract Price is NULL");
                        privalue = false;
                    }
                    if (privalue) {

                    } else {
                        $.ApplayContract.changeElement();
                        return privalue;
                    }
                }
                var data = jForm.getfrmData($("#applayForm"));
                data["saveType"] = type;
                data["fileIds_mine"] = $("#fileIds").val();
                data["fileIds_original"] = $("#OriginalFileIds").val();
                //data["fileIds-father"] = $("").val();
                $.ajax({
                    type: "POST",
                    url: "/ApplayContract/Save",
                    data: data,
                    dataType: "json",
                    success: function (result) {
                        $("#uploadLog").hide();
                        if (!result.msgHeader.IsError) {
                            jNotify.showSuccess("Successfully submitted",
                                function () {
                                    $(".login-error").hide();
                                    hideModal();//关闭模态框
                                    //console.log($("#ContractId").val());
                                    //根据入口不同返回页不同
                                    if ($("#ContractId").val() != "") {
                                        jqGrid.gridSearch($("#sel_form"), "/ApplayContract/ContractDrafts");
                                    }
                                    else {
                                        //jqGrid.gridSearch($("#sel_form"), "/Home/Index");
                                        window.location = "/Home/Index";
                                    }
                                });
                        } else {
                            $.ApplayContract.changeElement();
                            jNotify.showError(result.msgHeader.Message);
                        }
                    }
                });
            }
            else {
                $.ApplayContract.changeElement();
            }
        },

        //初始化
        initData: function () {

            $.ApplayContract.getContractTypeList();
            $("#ContractTypeId").change(function () {
                $.ApplayContract.contractTypeChange();
            });
        }
    }

    $.ApplayContract.initData();
})(jQuery);