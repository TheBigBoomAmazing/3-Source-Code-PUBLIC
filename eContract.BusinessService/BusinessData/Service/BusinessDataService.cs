using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eContract.BusinessService.BusinessData.BusinessRule;

namespace eContract.BusinessService.BusinessData.Service
{
    public class BusinessDataService
    {
        private static ContractTypeManagementBLL _contractTypeManagementService;
        public static ContractTypeManagementBLL ContractTypeManagementService
            => _contractTypeManagementService ?? (_contractTypeManagementService = new ContractTypeManagementBLL());

        private static ContractApplayBLL _contractApplayService;
        public static ContractApplayBLL ContractApplayService
            => _contractApplayService ?? (_contractApplayService = new ContractApplayBLL());

        private static POBLL _POService;
        public static POBLL POService
            => _POService ?? (_POService = new POBLL());

        private static ProxyApprovalBLL _proxyApprovalService;
        public static ProxyApprovalBLL ProxyApprovalService
            => _proxyApprovalService ?? (_proxyApprovalService = new ProxyApprovalBLL());

        private static DepUserCommonBLL _depUserCommonService;
        public static DepUserCommonBLL DepUserCommonService=> 
            _depUserCommonService?? (_depUserCommonService = new DepUserCommonBLL());

        private static ContractApprovalSetBLL _contractApprovalSetService;
        public static ContractApprovalSetBLL ContractApprovalSetService => _contractApprovalSetService ?? (_contractApprovalSetService = new ContractApprovalSetBLL());

        private static POApprovalSetBLL _pOApprovalSetService;
        public static POApprovalSetBLL POApprovalSetService => _pOApprovalSetService ?? (_pOApprovalSetService = new POApprovalSetBLL());

        private static ContractTemplateBLL _contractTemplateService;
        public static ContractTemplateBLL ContractTemplateService => _contractTemplateService ?? (_contractTemplateService = new ContractTemplateBLL());

        private static ContractManagementBLL _contractManagementService;

        public static ContractManagementBLL ContractManagementService => _contractManagementService ?? (_contractManagementService = new ContractManagementBLL());

        private static ContractCommentBLL _contractCommentService;
        public static ContractCommentBLL ContractCommentService => _contractCommentService ?? (_contractCommentService = new ContractCommentBLL());

        private static CommonHelperBLL _commonHelperService;
        public static CommonHelperBLL CommonHelperService => _commonHelperService ?? (_commonHelperService = new CommonHelperBLL());

        private static ContractApprovalBLL _contractApprovalService;
        public static ContractApprovalBLL ContractApprovalService => _contractApprovalService ?? (_contractApprovalService = new ContractApprovalBLL());

        public static ContractFieldBLL _contractFieldService;
        public static ContractFieldBLL ContractFieldService => _contractFieldService ?? (_contractFieldService = new ContractFieldBLL());
        #region 磐石系统相关代码
        public static LubrProductsShowBLL _lubrProductsShowService;
        public static LubrProductsShowBLL LubrProductsShowBLLService => _lubrProductsShowService ?? (_lubrProductsShowService = new LubrProductsShowBLL());

        public static LubrRegisterBLL _lubrRegisterService;
        public static LubrRegisterBLL LubrRegisterService => _lubrRegisterService ?? (_lubrRegisterService = new LubrRegisterBLL());

        //获取验证码
        public static LubrPhoneSMSCodeBLL _lubrPhoneSMSCodeService;
        public static LubrPhoneSMSCodeBLL LubrPhoneSMSCodeService => _lubrPhoneSMSCodeService ?? (_lubrPhoneSMSCodeService = new LubrPhoneSMSCodeBLL());

        #endregion
    }
}
