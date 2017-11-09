(function ($) {
    $.HistoryContract = {
        contractTypeList: {},//当前用户所有可申请的合同类型
        currentContractTypeEntity: null,//当前页面显示的合同类型

        //获取合同类型，并绑定费列罗方的change事件
        getContractTypeList: function () {
            var contractTypeListUrl = "/HistoryContract/GetContractTypeList";
            if ($("#hidEditType").val() == "View") {
                contractTypeListUrl = "/HistoryContract/GetAllContractTypeList";
            }
            $.ajax({
                url: contractTypeListUrl,
                method: "get",
                dataType: "json",
                async: false,
                data: {},
                success: function (result) {
                    $.HistoryContract.contractTypeList = eval("(" + result + ")");
                    $("#FerreroEntity").change(function () {
                        var contractFroupHtml = '<option value="">===Select===</option>';
                        $("#ContractTypeId").empty();
                        for (var i = 0; i < $.HistoryContract.contractTypeList.length; i++) {
                            var item = $.HistoryContract.contractTypeList[i];
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
            for (var i = 0; i < $.HistoryContract.contractTypeList.length; i++) {
                if (selContractTypeId == $.HistoryContract.contractTypeList[i].ContractTypeId) {
                    $.HistoryContract.currentContractTypeEntity = $.HistoryContract.contractTypeList[i];
                    break;
                }
            }
            if ($.HistoryContract.currentContractTypeEntity == null) {
                jNotify.showError("The contract type is exception, please refresh the page.");
                return;
            }
            //控制是否显示模板页
            if ($.HistoryContract.currentContractTypeEntity.IsTemplateContract) {
                $("#templateContract").css("display", "block");
                $("#normalContract").css("display", "none");
            } else {
                $("#templateContract").css("display", "none");
                $("#normalContract").css("display", "block");

            }
            //控制页面哪些字段显示
            var attrArray = [$.HistoryContract.currentContractTypeEntity.CounterpartyEn
                , $.HistoryContract.currentContractTypeEntity.CounterpartyCn
                , $.HistoryContract.currentContractTypeEntity.ContractName
                , $.HistoryContract.currentContractTypeEntity.ContractTerm
                , $.HistoryContract.currentContractTypeEntity.ContractOwner//add
                , $.HistoryContract.currentContractTypeEntity.ContractInitiator//add
                , $.HistoryContract.currentContractTypeEntity.ApplyDate//add
                , $.HistoryContract.currentContractTypeEntity.Capex
                , $.HistoryContract.currentContractTypeEntity.IsMasterAgreement
                , $.HistoryContract.currentContractTypeEntity.Supplementary
                , $.HistoryContract.currentContractTypeEntity.BudgetType
                , $.HistoryContract.currentContractTypeEntity.TemplateNo
                //, $.HistoryContract.currentContractTypeEntity.ContractPrice
                //, $.HistoryContract.currentContractTypeEntity.EstimatedPrice
                , $.HistoryContract.currentContractTypeEntity.Currency
                , $.HistoryContract.currentContractTypeEntity.PrepaymentAmount
                , $.HistoryContract.currentContractTypeEntity.PrepaymentPercentage
                , $.HistoryContract.currentContractTypeEntity.ContractKeyPoints
                , $.HistoryContract.currentContractTypeEntity.TemplateNo
                , $.HistoryContract.currentContractTypeEntity.TemplateName
                , $.HistoryContract.currentContractTypeEntity.TemplateTerm
                , $.HistoryContract.currentContractTypeEntity.TemplateOwner//add
                , $.HistoryContract.currentContractTypeEntity.TemplateInitiator//add
                , $.HistoryContract.currentContractTypeEntity.ScopeOfApplication
                , $.HistoryContract.currentContractTypeEntity.HasAttachment
                , $.HistoryContract.currentContractTypeEntity.BudgetAmount
                , $.HistoryContract.currentContractTypeEntity.InternalORInvestmentOrder
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
            $.HistoryContract.isShowElement(attrArray, elemetContentIdArray, $.HistoryContract.currentContractTypeEntity.IsTemplateContract);

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
        },

        //私有方法：save控制属性变更
        changeElement: function () {
            var btnObj = $("#btnSubmit");
            if (typeof (btnObj) != typeof (undefined))
            {
                $("#btnSubmit").removeAttr("disabled");
            }
        },
        //保存
        save: function (type) {
            var nameValue = false;
            if ($.HistoryContract.currentContractTypeEntity.ContractName && $("#selected_ContractName").val() == "") {
                jNotify.showError("Please select Contract Name !");
                nameValue = false;
                return nameValue;
            }
            var valid = $("#applayForm").valid();
            console.info(valid)
            if (valid) {
                //合同原件是否上传校验
                if ($.HistoryContract.currentContractTypeEntity.HasAttachment && $("#fileIds").val() == "") {
                    jNotify.showError("Please upload the original contract.");
                    $.HistoryContract.changeElement();
                    return;
                }
                ////如果是补充合同，校验
                //if ($.HistoryContract.currentContractTypeEntity.Supplementary) {
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
                        $.HistoryContract.changeElement();
                        return;
                    }
                    if ($("#selOriginalContract").val() != "" && $("#OriginalFileIds").val() != "") {
                        jNotify.showError("Please upload or choose the contract.");
                        $.HistoryContract.changeElement();
                        return;
                    }
                }
                //是否需要领导批注且申请用户是否有领导
                if (type == "2") {
                    var outresult;
                    $.ajax({
                        type: "Get",
                        async: false,
                        url: "/HistoryContract/CheckLeaderExist",
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
                        $.HistoryContract.changeElement();
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
                    $.HistoryContract.changeElement();
                    return false;
                }
                //申请人是否有部门总监
                if (type == "2") {
                    var outresult;
                    $.ajax({
                        type: "Get",
                        async: false,
                        url: "/HistoryContract/CheckDEPManagerExist",
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
                        $.HistoryContract.changeElement();
                        return outresult;
                    }

                }

                //申请人是否有大区总监
                if (type == "2") {
                    var outresult;
                    $.ajax({
                        type: "Get",
                        async: false,
                        url: "/HistoryContract/CheckRegionManagerExist",
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
                        $.HistoryContract.changeElement();
                        return outresult;
                    }

                }
                //申请人是否有用户领导
                if (type == "2") {
                    var outresult;
                    $.ajax({
                        type: "Get",
                        async: false,
                        url: "/HistoryContract/CheckLineManagerExist",
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
                        $.HistoryContract.changeElement();
                        return outresult;
                    }
                }

                //校验模板合同的模板编号是否重复  $("#TemplateNoForInput").val()
                if ($.HistoryContract.currentContractTypeEntity.IsTemplateContract) {
                    var outresult;
                    $.ajax({
                        type: "Get",
                        async: false,
                        url: "/HistoryContract/CheckTemplateNo",
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
                        $.HistoryContract.changeElement();
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
                        $.HistoryContract.changeElement();
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
                        $.HistoryContract.changeElement();
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
                    url: "/HistoryContract/Save",
                    data: data,
                    dataType: "json",
                    success: function (result) {
                        $("#uploadLog").hide();
                        if (!result.msgHeader.IsError) {
                            jNotify.showSuccess("Successfully submitted",
                                function () {
                                    $(".login-error").hide();
                                    hideModal();//关闭模态框
                                    //jqGrid.gridSearch($("#sel_form"), "/CAS/HistoryContract");
                                    jqGrid.grid.jqGrid().trigger("reloadGrid");
                                    //window.location = "/CAS/HistoryContract";
                                });
                        } else {
                            $.HistoryContract.changeElement();
                            jNotify.showError(result.msgHeader.Message);
                        }
                    }
                });
            }
            else {
                $.HistoryContract.changeElement();
            }
        },

        //初始化
        initData: function () {

            $.HistoryContract.getContractTypeList();
            $("#ContractTypeId").change(function () {
                $.HistoryContract.contractTypeChange();
            });
        }
    }

    $.HistoryContract.initData();
})(jQuery);