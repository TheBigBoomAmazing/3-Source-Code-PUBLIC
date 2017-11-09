//数字验证（Number）      
jQuery.validator.addMethod("IsNumber", function (value, element) {
	return this.optional(element) || /^[0-9]+$/.test(value);
}, "请输入有效数字");

//字符验证      
jQuery.validator.addMethod("stringCheck", function (value, element) {
	return this.optional(element) || /^[\u0391-\uFFE5\w]+$/.test(value);
}, "请输入有效的中文");
//字符验证      
jQuery.validator.addMethod("stringEn", function (value, element) {
    return this.optional(element) || /^[a-zA-Z0-9_\.\s]+$/.test(value);
}, "请输入有效的字符格式");
