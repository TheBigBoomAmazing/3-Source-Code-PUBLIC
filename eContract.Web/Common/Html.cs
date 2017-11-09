using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Web.Mvc.Html;
using System.Text;
using System.Linq.Expressions;

using System.Data;

using System.Text.RegularExpressions;
using eContract.Web;

/// <summary>
/// 自定义grid控件
/// </summary>
public static class HtmlExtensions
{
    const string ValidationErrorCssClassName = " has-error";
    public static MvcHtmlString TextBoxVaild(this HtmlHelper htmlHelper)
    {
        StringBuilder html = new StringBuilder();
      
        html.Append("<div class=\"controls clearfix\">");
        html.Append("<div class=\"input-group input-group-lg\">");
        html.Append("<input class=\"form-control input-lg\" type=\"text\"  placeholder=\"密码\" id=\"Code\" maxlength=\"4\" name=\"Code\" validate=\"{required:true,minlength:4,maxlength:4}\" > ");
        html.Append("<span class=\"input-group-btn\"><button class=\"btn btn-default btn-validcode\"  title=\"验证码\" type=\"button\"  onclick=\"CodeChange()\"> <img class=\"login-validcode\" id=\"imgcode\" src=\"../Account/getCode\" /></button> </span>");
        html.Append("</div></div>");
        return MvcHtmlString.Create(html.ToString());
    }

    #region XTextBox
    public static MvcHtmlString XTextBox(this HtmlHelper htmlHelper, string name, xInputType type)
    {
        return XTextBox(htmlHelper, name, type, "");
    }

    public static MvcHtmlString XTextBox(this HtmlHelper htmlHelper, string name, xInputType type, string strIcon)
    {
        return XTextBox(htmlHelper, name, type, strIcon, new UIValidation());
    }

    public static MvcHtmlString XTextBox(this HtmlHelper htmlHelper, string name, xInputType type,string strIcon, UIValidation valid)
    {
        return XTextBox(htmlHelper, name, type, null,strIcon, valid, null);
    }

    public static MvcHtmlString XTextBox(this HtmlHelper htmlHelper, string name, xInputType type, object value, string strIcon, UIValidation valid)
    {
        return XTextBox(htmlHelper, name, type, value, strIcon, valid, null);
    }

    public static MvcHtmlString XTextBox(this HtmlHelper htmlHelper, string name, xInputType type, object value, UIValidation valid, object htmlAttribute)
    {
        return XTextBox(htmlHelper, name, type, value, "", valid, htmlAttribute);
    }

    /// <summary>
    /// 文本框控件
    /// </summary>
    /// <param name="htmlHelper"></param>
    /// <param name="name">控件id</param>
    /// <param name="type">控件类别</param>
    /// <param name="valid">验证</param>
    /// <param name="htmlAttribute"></param>
    /// <returns></returns>
    public static MvcHtmlString XTextBox(this HtmlHelper htmlHelper, string name, xInputType type, object value, string strIcon, UIValidation valid, object htmlAttribute)
    {
        StringBuilder html = new StringBuilder();
        var className = "form-group";
       
        string msgError = "";
        IDictionary<string, object> htmlAttributes = htmlAttribute != null ? (IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttribute) : null;
        if (IsPost && IsError(htmlHelper, name, out msgError))
        {
            className += ValidationErrorCssClassName;
        }
        if ((value == null || string.IsNullOrEmpty(value.ToString()))&&valid!=null&&!string.IsNullOrEmpty(valid.DefaultValue))
        {
            value = valid.DefaultValue;
        }
        if (type == xInputType.hidden)
        {
            html.Append("<input id='"
           + name + "' " + (value != null ? "value='" + value + "'" : "") + " name='" + name + "' type='" + type.ToString() + "' ");
            return MvcHtmlString.Create(html.ToString());
        }
      
        html.Append("\r\n<div id='div_" + name + "' class='" + className + "'>");
        if (type != xInputType.hidden&&valid!=null&&!string.IsNullOrEmpty(valid.DisplayName))
        {
            html.Append("<label for='" + name + "' class='col-sm-"+(valid.LabelLength)+" control-label'>");
           
            if (valid.required)
            {
                html.Append("<span class='asteriskField'>*</span>");
            }
            else
            {
                html.Append("<span class='asteriskField'>&nbsp;</span>");
            }
            html.Append(valid.DisplayName+ "：");
            html.Append("</label>");
        }
        html.Append("<div class='col-sm-" + (12-valid.LabelLength) + " controls '>");

         if (valid != null && valid.disabled && type != xInputType.hidden)
        {
            html.Append("<input id='"
           + name + "' " + (value != null ? "value='" + value + "'" : "") + " name='" + name + "' type='hidden' />");
        }
         html.Append("<input class='textinput form-control " + (htmlAttributes != null && htmlAttributes.Keys.Contains("StyleClass") ? htmlAttributes["StyleClass"] : "")
            //+"' placeholder='" + (valid != null ? valid.DisplayName : "")   //去除placeholder显示
            + "' " + (value != null ? "value='" + value.ToString() + "'" : "") + " type='" + type.ToString() + "' ");
        if (valid != null && valid.disabled && type != xInputType.hidden)
        {
            html.Append(" id='" + name + "_disabled' name='" + name + "_disabled' disabled='disabled'");
        }
        else
        {
            html.Append(" id='" + name + "' name='" + name + "' ");
        }
        if (type != xInputType.hidden)
        {
            html.Append( (valid != null ? valid.ToJson() : "") );
        }
        html.Append(TagAttribute(htmlAttributes));
        html.Append("/>");
        if (!string.IsNullOrEmpty(strIcon))
        {
            html.Append("<span class='input-group-addon'>" + strIcon + "</span>");
        }
         
       
        html.Append("<p class='text-danger help-block'>" + msgError + "</p></div>");

        html.Append("</div>");
        return MvcHtmlString.Create(html.ToString());
    }


    public static MvcHtmlString XTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
        Expression<Func<TModel, TProperty>> expression,
         UIValidation valid)
    {
        return XTextBoxFor(htmlHelper, expression,"",valid);
    }

    public static MvcHtmlString XTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
       Expression<Func<TModel, TProperty>> expression,
        string strIcon,
        UIValidation valid
       )
    {
        return XTextBoxFor(htmlHelper, expression, strIcon, valid,null);
    }
    public static MvcHtmlString XTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
       Expression<Func<TModel, TProperty>> expression,
        UIValidation valid,
        object htmlAttribute
       )
    {
        return XTextBoxFor(htmlHelper, expression,"", valid, htmlAttribute);
    }
    public static MvcHtmlString XTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
       Expression<Func<TModel, TProperty>> expression,
        string strIcon,
        UIValidation valid,
        object htmlAttribute
       )
    {
        return XTextBoxFor(htmlHelper, expression, xInputType.text, strIcon, valid, htmlAttribute);
    }
    /// <summary>
    /// 实体绑定控件
    /// </summary>
    /// <typeparam name="TModel">实体映射</typeparam>
    /// <typeparam name="TProperty">属性</typeparam>
    /// <param name="htmlHelper">页面对象</param>
    /// <param name="expression"></param>
    /// <param name="type">类别</param>
    /// <param name="strIcon">图标</param>
    /// <param name="valid">验证</param>
    /// <param name="htmlAttribute">附加属性</param>
    /// <returns></returns>
    public static MvcHtmlString XTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, 
        Expression<Func<TModel, TProperty>> expression,
        xInputType type,
        string strIcon,
        UIValidation valid,
        object htmlAttribute)
    {
        ModelMetadata metadata = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, htmlHelper.ViewData);
        //metadata.GetDisplayName()
        return htmlHelper.XTextBox(metadata.GetDisplayName(), type, metadata.Model, strIcon, valid, htmlAttribute);
    }
    #endregion

    #region hidden
    public static MvcHtmlString XHidden(this HtmlHelper htmlHelper,
      string name, object data)
    {
        return MvcHtmlString.Create("<input type='hidden' name='"+name+"' id='"+name+"' "+(data!=null&&!string.IsNullOrEmpty(data.ToString())?"value='"+data+"'":"")+"></input>");
    }
    public static MvcHtmlString XHiddenFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
       Expression<Func<TModel, TProperty>> expression)
    {
        ModelMetadata metadata = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, htmlHelper.ViewData);
        return XHidden(htmlHelper,metadata.GetDisplayName(), metadata.Model);
    }
    #endregion

    #region XDropDownList
    public static MvcHtmlString XDropDownList(this HtmlHelper htmlHelper, string name, bool IsNull, object data)
    {
        return XDropDownList(htmlHelper, name, null, true, data, new UIValidation());
    }
    public static MvcHtmlString XDropDownList(this HtmlHelper htmlHelper, string name, object model, bool IsNull, object data)
    {
        return XDropDownList(htmlHelper, name, model, true, data, new UIValidation());
    }
    public static MvcHtmlString XDropDownList(this HtmlHelper htmlHelper, string name, object model, bool IsNull, object data, UIValidation valid)
    {
        return XDropDownList(htmlHelper, name, model, IsNull, data, valid, null);
    }
    /// <summary>
    /// 下拉框控件
    /// </summary>
    /// <param name="htmlHelper"></param>
    /// <param name="name">控件名称</param>
    /// <param name="data">数据集合</param>
    /// <param name="valid">验证对象</param>
    /// <param name="htmlAttribute">控件属性</param>
    /// <returns></returns>
    public static MvcHtmlString XDropDownList(this HtmlHelper htmlHelper, string name,object value,bool IsNull,object data,UIValidation valid, object htmlAttribute)
    {
        StringBuilder html = new StringBuilder();

        var className = "form-group";
        string msgError = "";
        string displayName = "text";
        string displayValue = "id";
        string defaultValue = "";
        IDictionary<string, object> htmlAttributes = htmlAttribute != null ? (IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttribute) : null;
        if (IsPost && IsError(htmlHelper, name , out msgError))
        {
            className += ValidationErrorCssClassName;
        }
        if (value == null || string.IsNullOrEmpty(value.ToString()) )
        {
            value = (valid!=null)?valid.DefaultValue:null;
        }
        
        if (htmlAttributes != null)
        {
            if (htmlAttributes.Keys.Contains("displayName"))
            {
                displayName = htmlAttributes["displayName"].ToString();
            }
            if (htmlAttributes.Keys.Contains("displayValue"))
            {
                displayValue = htmlAttributes["displayValue"].ToString();
            }
            if (htmlAttributes.Keys.Contains("defaultValue"))
            {
                defaultValue = htmlAttributes["defaultValue"].ToString();
            }
        }
        html.Append("\r\n<div id='div_" + name + "' class='" + className + "'>");
        if (valid != null && !string.IsNullOrEmpty(valid.DisplayName))
        {
            html.Append("<label for='" + name + "' class='col-sm-2 control-label'>");
           
            if (valid != null && valid.required)
            {
                html.Append("<span class='asteriskField'>*</span>");
            }
            else
            {
                html.Append("<span class='asteriskField'>&nbsp;</span>");
            }
            html.Append((valid != null ? valid.DisplayName+"：" : ""));
            html.Append("</label>");
        } 
        html.Append("<div class='col-sm-10 controls '>");
        string href= (htmlAttributes != null && htmlAttributes.Keys.Contains("href") ? htmlAttributes["href"].ToString() : "");
        if (!string.IsNullOrEmpty(href))
        {
            html.Append("<a href='" + href + "' title='创建新的 " + (valid != null ? valid.DisplayName : "") + "' class='btn btn-primary btn-sm btn-ajax pull-right' data-for-id='id_host' data-refresh-url='" + (htmlAttributes != null && htmlAttributes.Keys.Contains("refreshurl") ? htmlAttributes["refreshurl"] : "") + "'><i class='icon-plus'></i></a>");
        }
        //html.Append("<div class='control-wrap' id='id_"+name+"_wrap_container'>");
        if (valid != null && valid.disabled)
        {
            html.Append(" <input type='hidden' id='" + name + "' name='" + name + "' " +((value != null && !string.IsNullOrWhiteSpace(value.ToString())) ? "value='" + value.ToString()+ "'" : "") + " /> ");
        }
        html.Append("<select class='form-control m-b c-edit-select valid' ");
        if (valid != null && valid.disabled)
        {
            html.Append(" id='id_" + name + "_disabled' name='" + name + "_disabled' disabled='disabled'");
        }
        else
        {
            html.Append(" id='" + name + "' name='" + name + "' ");
        }
        html.Append(valid != null ? valid.ToJson() : "");
        html.Append(TagAttribute(htmlAttributes));
        html.Append(">");
        #region 遍历数据
        if (IsNull)
        {
            html.Append("<option value='" + defaultValue + "' " + (value != null && defaultValue.Equals(value) ? "selected='selected'" : "") + ">&nbsp;</option>");
        }
        string keyvalue = (value!=null?value.ToString():"");
        if (data is DataTable)
        {
            DataTable dt = (DataTable)data;
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    if (dt.Columns.Contains(displayName)&&item[displayName]!=null)
                    {
                        html.Append("<option value='" + (item[displayValue] != null ? item[displayValue] : item[displayName]) + "' " + ((value != null && ((item[displayValue] != null && item[displayValue].Equals(keyvalue)) || (item[displayValue] == null && item[displayName].Equals(keyvalue)))) ? "selected='selected'" : "") + ">" + item[displayName].ToString() + "</option>");
                    }
                }
            }
        }
       
        else if (data is Dictionary<string, string>)
        {
            Dictionary<string, string> dic = (Dictionary<string, string>)data;
            if (dic != null)
            {
                foreach (var key in dic.Keys)
                {
                    string display = "&nbsp;";
                    if (!string.IsNullOrEmpty(dic[key]))
                    {
                        display = dic[key];
                        if (valid != null && valid.ValueTranslate && !string.IsNullOrEmpty(display))
                        {
                            display =dic[key];
                        }
                    }
                    html.Append("<option value='" + key + "' " + ((value != null && key.Equals(keyvalue)) ? "selected='selected'" : "") + " >" + (!string.IsNullOrEmpty(display) ? display : "&nbsp;") + "</option>");
                }
            }
        }
        else if (data is Dictionary<int, string>)
        {
            Dictionary<int, string> dic = (Dictionary<int, string>)data;
            if (dic != null)
            {
                foreach (var key in dic.Keys)
                {
                    string display = "&nbsp;";
                    if (!string.IsNullOrEmpty(dic[key]))
                    {
                        display = dic[key];
                        if (valid != null && valid.ValueTranslate && !string.IsNullOrEmpty(display))
                        {
                            display = dic[key];
                        }
                    }
                    html.Append("<option value='" + key + "' " + ((value != null && key.ToString().Equals(keyvalue)) ? "selected='selected'" : "") + " >" + (!string.IsNullOrEmpty(display) ? display : "&nbsp;") + "</option>");
                }
            }
        }
        else if (data is Dictionary<string, object>)
        {
            Dictionary<string, object> dic = (Dictionary<string, object>)data;
            if (dic != null)
            {
                foreach (var key in dic.Keys)
                {
                    html.Append("<option value='" + key + "' " + (value != null && key.Equals(keyvalue) ? "selected='selected'" : "") + " >" + (!string.IsNullOrEmpty(dic[key].ToString()) ? dic[key].ToString() : "&nbsp;") + "</option>");
                }
            }
        }
        else if (data is List<string>)
        {
            List<string> dic = (List<string>)data;
            if (dic != null)
            {
                foreach (var key in dic)
                {
                    if (string.IsNullOrEmpty(key))
                    {
                        continue;
                    }
                    html.Append("<option value='" + key + "' " + (value != null && key.Equals(keyvalue) ? "selected='selected'" : "") + ">" + (!string.IsNullOrEmpty(key) ? key : "&nbsp;") + "</option>");
                }
            }
        }
        else if (data!=null&&data.GetType().IsArray)
        {
            var dic=data as Array;
            if (dic.Length>0)
            {
                foreach (var item in dic)
                {
                    string value1 =((item.GetType().GetProperty(displayValue)!=null&&item.GetType().GetProperty(displayValue).GetValue(item)!=null)?item.GetType().GetProperty(displayValue).GetValue(item).ToString():"");
                    string display = ((item.GetType().GetProperty(displayValue) != null && item.GetType().GetProperty(displayName).GetValue(item) != null) ? item.GetType().GetProperty(displayName).GetValue(item).ToString() : "");
                    
                    html.Append("<option value='" + value1 + "' " + (value != null && value1.Equals(keyvalue) ? "selected='selected'" : "") + ">" + (!string.IsNullOrEmpty(display) ? display : "&nbsp;") + "</option>");
                }
                //foreach (var key in dic)
                //{
                //    html.Append("<option value='" + key + "' " + (value != null && key.Equals(value) ? "selected='selected'" : "") + ">" + (!string.IsNullOrEmpty(key) ? key : "&nbsp;") + "</option>");
                //}
            }
        }
        #endregion
        html.Append("</select>");
        // html.Append("<p class='text-danger help-block'>" + msgError + "</p></div></div>");
        html.Append("<p class='text-danger help-block'>" + msgError + "</p></div>");
        html.Append("</div>");
        return MvcHtmlString.Create(html.ToString());
    }

    public static MvcHtmlString XDropDownListGroup(this HtmlHelper htmlHelper, string name, object value, bool IsNull, object data, UIValidation valid, object htmlAttribute)
    {
        StringBuilder html = new StringBuilder();
       
        var className = "form-group";
        string msgError = "";
        string displayName = "text";
        string displayValue = "id";
        string defaultValue = "";
        IDictionary<string, object> htmlAttributes = htmlAttribute != null ? (IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttribute) : null;
        if (IsPost && IsError(htmlHelper, name, out msgError))
        {
            className += ValidationErrorCssClassName;
        }
        if (value == null || string.IsNullOrEmpty(value.ToString()))
        {
            value = (valid != null) ? valid.DefaultValue : null;
        }

        if (htmlAttributes != null)
        {
            if (htmlAttributes.Keys.Contains("displayName"))
            {
                displayName = htmlAttributes["displayName"].ToString();
            }
            if (htmlAttributes.Keys.Contains("displayValue"))
            {
                displayValue = htmlAttributes["displayValue"].ToString();
            }
            if (htmlAttributes.Keys.Contains("defaultValue"))
            {
                defaultValue = htmlAttributes["defaultValue"].ToString();
            }
        }
        html.Append("\r\n<div id='div_" + name + "' class='" + className + "'>");
        if (valid != null && !string.IsNullOrEmpty(valid.DisplayName))
        {
            html.Append("<label for='" + name + "' class='control-label'>");
            html.Append((valid != null ? valid.DisplayName : ""));
            if (valid != null && valid.required)
            {
                html.Append("<span class='asteriskField'>*</span>");
            }
            else
            {
                html.Append("<span class='asteriskField'>&nbsp;</span>");
            }
            html.Append("</label>");
        }
        html.Append("<div class='controls '>");
        string href = (htmlAttributes != null && htmlAttributes.Keys.Contains("href") ? htmlAttributes["href"].ToString() : "");
        if (!string.IsNullOrEmpty(href))
        {
            html.Append("<a href='" + href + "' title='创建新的 " + (valid != null ? valid.DisplayName : "") + "' class='btn btn-primary btn-sm btn-ajax pull-right' data-for-id='id_host' data-refresh-url='" + (htmlAttributes != null && htmlAttributes.Keys.Contains("refreshurl") ? htmlAttributes["refreshurl"] : "") + "'><i class='icon-plus'></i></a>");
        }
        html.Append("<div class='control-wrap' id='id_" + name + "_wrap_container'>");
        if (valid != null && valid.disabled)
        {
            html.Append(" <input type='hidden' id='id_" + name + "_input' name='" + name + "' " + ((value != null && !string.IsNullOrWhiteSpace(value.ToString())) ? "value='" + value.ToString() + "'" : "") + " /> ");
        }
        html.Append("<select ");
        if (valid != null && valid.disabled)
        {
            html.Append(" id='id_" + name + "_disabled' name='" + name + "_disabled' disabled='disabled'");
        }
        else
        {
            html.Append(" id='" + name + "' name='" + name + "' ");
        }
        html.Append(valid != null ? valid.ToJson() : "");
        html.Append(TagAttribute(htmlAttributes));
        html.Append(">");
        #region 遍历数据
        if (IsNull)
        {
            html.Append("<option value='' " + (value != null && "".Equals(value) ? "selected='selected'" : "") + ">&nbsp;</option>");
        }
        string keyvalue = (value != null ? value.ToString() : "");
        
        if (data != null && data.GetType().IsArray)
        {
            var dic = data as Array;
            if (dic.Length > 0)
            {
                foreach (var item in dic)
                {
                    if (item is Dictionary<string, Array>)
                    {
                        Dictionary<string, Array> a = (Dictionary<string, Array>)item;
                        if (a != null)
                        {
                           
                            foreach (var key in a.Keys)
                            {
                                html.Append("<optgroup label='" + key + "'>");
                                if (a[key] != null&&a[key].GetType().IsArray)
                                {
                                    var arr=  a[key] as Array;
                                    foreach (var index in arr)
                                    {
                                        string value1 = ((index.GetType().GetProperty(displayValue) != null && index.GetType().GetProperty(displayValue).GetValue(index) != null) ? index.GetType().GetProperty(displayValue).GetValue(index).ToString() : "");
                                        string display = ((index.GetType().GetProperty(displayValue) != null && index.GetType().GetProperty(displayName).GetValue(index) != null) ? index.GetType().GetProperty(displayName).GetValue(index).ToString() : "");
                                       
                                        html.Append("<option value='" + value1 + "' " + (value != null && value1.Equals(keyvalue) ? "selected='selected'" : "") + ">" + (!string.IsNullOrEmpty(display) ? display : "&nbsp;") + "</option>");
                                    }
                                }
                                html.Append("</optgroup>");
                            }
                         
                        }
                    }
                   
                }
                //foreach (var key in dic)
                //{
                //    html.Append("<option value='" + key + "' " + (value != null && key.Equals(value) ? "selected='selected'" : "") + ">" + (!string.IsNullOrEmpty(key) ? key : "&nbsp;") + "</option>");
                //}
            }
        }
        #endregion
        html.Append("</select>");
        html.Append("<p class='text-danger help-block'>" + msgError + "</p></div></div>");
        html.Append("</div>");
        return MvcHtmlString.Create(html.ToString());
    }

    public static MvcHtmlString XDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
       Expression<Func<TModel, TProperty>> expression,
        object data,
        UIValidation valid,
        object htmlAttribute)
    {
        ModelMetadata metadata = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, htmlHelper.ViewData);
        return htmlHelper.XDropDownList(metadata.GetDisplayName(), metadata.Model, false, data, valid, htmlAttribute);
    }

    public static MvcHtmlString XDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
       Expression<Func<TModel, TProperty>> expression,
       bool IsNull,
        object data,
        UIValidation valid, 
        object htmlAttribute)
    {
        ModelMetadata metadata = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, htmlHelper.ViewData);
        return htmlHelper.XDropDownList(metadata.GetDisplayName(), metadata.Model, IsNull, data, valid, htmlAttribute);
    }

    public static MvcHtmlString XDropDownListGroupFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
       Expression<Func<TModel, TProperty>> expression,
       bool IsNull,
        object data,
        UIValidation valid,
        object htmlAttribute)
    {
        ModelMetadata metadata = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, htmlHelper.ViewData);
        return htmlHelper.XDropDownListGroup(metadata.GetDisplayName(), metadata.Model, IsNull, data, valid, htmlAttribute);
    }
    #endregion

    #region XTextArea
    public static MvcHtmlString XTextArea(this HtmlHelper htmlHelper, string name)
    {
        return XTextArea(htmlHelper, name, null);
    }
    public static MvcHtmlString XTextArea(this HtmlHelper htmlHelper, string name,object model)
    {
        return XTextArea(htmlHelper, name, model, new UIValidation());
    }
    public static MvcHtmlString XTextArea(this HtmlHelper htmlHelper, string name, object model, UIValidation valid)
    {
        return XTextArea(htmlHelper, name, model, valid, null);
    }

    /// <summary>
    /// 文本域控件
    /// </summary>
    /// <param name="htmlHelper"></param>
    /// <param name="name">id</param>
    /// <param name="model">实体对象</param>
    /// <param name="valid">验证对象</param>
    /// <param name="htmlAttribute">附加属性</param>
    /// <returns></returns>
    public static MvcHtmlString XTextArea(this HtmlHelper htmlHelper, string name,object value,  UIValidation valid, object htmlAttribute)
    {
        StringBuilder html = new StringBuilder();
        TagBuilder tagBuilder = new TagBuilder("input");
        
        var className = "form-group";
        string msgError = "";
        IDictionary<string, object> htmlAttributes = htmlAttribute != null ? (IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttribute) : null;
        if (IsPost && IsError(htmlHelper, name, out msgError))
        {
            className += ValidationErrorCssClassName;
        }
        html.Append("\r\n<div id='div_" + name + "' class='" + className + "'>");
        if (valid != null && !string.IsNullOrEmpty(valid.DisplayName))
        {
            html.Append("<label for='" + name + "' class='col-sm-2 control-label'>");
           
            if (valid.required)
            {
                html.Append("<span class='asteriskField'>*</span>");
            }
            else
            {
                html.Append("<span class='asteriskField'>&nbsp;</span>");
            }
            html.Append(valid.DisplayName+"：");
            html.Append("</label>");
        }
        html.Append("<div class='col-sm-10 controls '>");
        if (valid != null && valid.disabled)
        {
            html.Append("<textarea id='" + name + "' name='" + name + "' style='display:none;'>" + (value != null ?  value.ToString() : "") + "</textarea>");
        }
        html.Append("<textarea class='textarea-field form-control " + (htmlAttributes != null && htmlAttributes.Keys.Contains("StyleClass") ? htmlAttributes["StyleClass"] : "") 
            //+ "' placeholder='" + (valid != null ? valid.DisplayName : "") //去掉placeholder
            + "' ");
        if (valid != null && valid.disabled )
        {
            html.Append(" id='" + name + "_disabled' name='" + name + "_disabled' disabled='disabled'");
        }
        else
        {
            html.Append(" id='" + name + "' name='" + name + "' ");
        }
        html.Append(TagAttribute(htmlAttributes));
        if (valid != null)
        {
            html.Append((valid != null ? valid.ToJson() : ""));
        }
        html.Append(" " + (valid != null && valid.disabled ? "disabled='disabled'" : "") + ">");
        html.Append((value != null ?  value.ToString() : ""));
        html.Append("</textarea><p class='text-danger help-block'>" + msgError + "</p></div>");

        html.Append("</div>");
        return MvcHtmlString.Create(html.ToString());
    }

    public static MvcHtmlString XTextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
      Expression<Func<TModel, TProperty>> expression,
      UIValidation valid)
    {
        ModelMetadata metadata = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, htmlHelper.ViewData);
        return htmlHelper.XTextArea(metadata.GetDisplayName(), metadata.Model, valid, null);
    }
    public static MvcHtmlString XTextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
       Expression<Func<TModel, TProperty>> expression,
       UIValidation valid,
       object htmlAttribute)
    {
        ModelMetadata metadata = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, htmlHelper.ViewData);
        return htmlHelper.XTextArea(metadata.GetDisplayName(), metadata.Model, valid, htmlAttribute);
    }
    #endregion

    #region XDateTime
    public static MvcHtmlString XDateTime(this HtmlHelper htmlHelper, string name)
    {
        return XDateTime(htmlHelper, name,new UIValidation());
    }

    public static MvcHtmlString XDateTime(this HtmlHelper htmlHelper, string name,  UIValidation valid)
    {
        return XDateTime(htmlHelper, name,null, false, valid, null);
    }

    /// <summary>
    /// 日期控件
    /// </summary>
    /// <param name="htmlHelper"></param>
    /// <param name="name"></param>
    /// <param name="model"></param>
    /// <param name="IsTime"></param>
    /// <param name="valid"></param>
    /// <param name="htmlAttribute"></param>
    /// <returns></returns>
    public static MvcHtmlString XDateTime(this HtmlHelper htmlHelper, string name,object value, bool IsTime, UIValidation valid, object htmlAttribute)
    {
        StringBuilder html = new StringBuilder();
        TagBuilder tagBuilder = new TagBuilder("input");
        
        var className = "form-group";
        string msgError = "";
        string timeError = "";
       
        object timevalue = null;
        IDictionary<string, object> htmlAttributes = htmlAttribute != null ? (IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttribute) : null;
        object datevalue = timevalue = value;
        if (IsPost && IsError(htmlHelper, name + (IsTime ? "_date" : ""), out msgError))
        {
            className += ValidationErrorCssClassName;
        }
        if (IsPost && IsTime && IsError(htmlHelper, name + "_time", out timeError))
        {
            if (className.IndexOf(ValidationErrorCssClassName)!=-1)
            {
                className += ValidationErrorCssClassName;
            }
        }

        string inputClassName = htmlAttributes != null && htmlAttributes.Keys.Contains("StyleClass") ? htmlAttributes["StyleClass"].ToString() : "";

        html.Append("\r\n<div id='div_" + name + "' class='" + className + "'>");
        if (valid != null && !string.IsNullOrEmpty(valid.DisplayName))
        {
            html.Append("<label for='" + name + "_date' class='control-label'>");
            html.Append((valid != null ? valid.DisplayName : ""));
            if (valid.required)
            {
                html.Append("<span class='asteriskField'>*</span>");
            }
            else
            {
                html.Append("<span class='asteriskField'>&nbsp;</span>");
            }
            html.Append("</label>");
        }
        html.Append("<div class='controls '>");
        html.Append("<div class='datetime clearfix '>");
        html.Append("<div class='input-group date bootstrap-datepicker'>");
        string strValue = "";
        if (datevalue != null)
        {
            strValue = Convert.ToDateTime(datevalue).ToString("yyyy-MM-dd");
        }
        if (valid != null && valid.disabled)
        {
            html.Append(" <input type='hidden' id='" + name + "' name='" + name + (IsTime ? "_date" : "") + "' " + (value != null ? "value='" + strValue + "'" : "") + " /> ");
        }
        html.Append("<input class='date-field form-control' size='10' type='text' ");
        if (valid != null && valid.disabled)
            html.Append(" id='" + name + "_date_disabled' name='" + name + (IsTime ? "_date" : "") + "_disabled' ");
        else
            html.Append(" id='" + name + "_date' name='" + name + (IsTime ? "_date" : "") + "' ");
        html.Append(((valid != null && valid.disabled) ? "disabled='disabled'" : ""));
        if (datevalue != null)
        {
            html.Append(" value='" + strValue + "' ");
        }
        Dictionary<string, object> dateRule = new Dictionary<string, object>();
        dateRule.Add("IsDate", true);
        html.Append(valid != null ? valid.ToJson(dateRule) : "");
        html.Append(TagAttribute(htmlAttributes));
        html.Append("/>");
        if (valid == null || !valid.disabled)
        {
            html.Append(" <span class='input-group-btn'>");
            html.Append("       <button class='btn btn-default' type='button' for='" + name + ((valid != null && valid.disabled) ? "_date_disabled" : "_date") + "' ><i class='icon-calendar'></i></button>");
            html.Append("    </span>");
        }
        html.Append("   </div>");
        if (IsTime)
        {
            html.Append("<div class='input-group time bootstrap-timepicker'>");
            Regex reg = new Regex("^[0-2][0-9]:[0-5][0-9]:[0-5][0-9]$");
            if (valid != null && valid.disabled)
            {
                html.Append(" <input type='hidden' id='" + name + "_time' name='" + name + "_time' " + (value != null ? "value='" + value + "'" : "") + " ");
                if (timevalue != null)
                {
                    html.Append(" value='" + (reg.IsMatch(timevalue.ToString()) ? timevalue.ToString() : Convert.ToDateTime(timevalue).ToString("HH:mm:ss")) + "' ");
                }
                html.Append("/> ");
            }
            html.Append("  <input class='date-field form-control' size='8' type='text' " + ((valid != null && valid.disabled) ? "disabled='disabled'" : ""));
            if (valid != null && valid.disabled)
                html.Append(" id='" + name + "_time_disabled' name='" + name + "_time_disabled' ");
            else
                html.Append("id='" + name + "_time' name='" + name + "_time' ");
            
            if (timevalue != null)
            {
                html.Append(" value='" + (reg.IsMatch(timevalue.ToString()) ? timevalue.ToString() : Convert.ToDateTime(timevalue).ToString("HH:mm:ss")) + "' ");
            }
            Dictionary<string, object> timeRule = new Dictionary<string, object>();
            timeRule.Add("IsTime", true);
            html.Append(valid != null ? valid.ToJson(timeRule) : "");

            html.Append(TagAttribute(htmlAttributes));
            html.Append(" />");
            if (valid == null || !valid.disabled)
            {
                html.Append("<span class='input-group-btn'>");
                html.Append("       <button class='btn btn-default' type='button'><i class='icon-time'></i></button>");
                html.Append("    </span>");
            }
            html.Append("   </div>");
        }
        html.Append("   </div>");
      
        
        html.Append("<p class='text-danger help-block'>" + msgError + "</p></div>");
     
        html.Append("</div>");
        return MvcHtmlString.Create(html.ToString());
    }

    public static MvcHtmlString XDateTimeFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
      Expression<Func<TModel, TProperty>> expression,
       UIValidation valid
       )
    {
        ModelMetadata metadata = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, htmlHelper.ViewData);
        return XDateTimeFor(htmlHelper, expression,valid,null);
    }
    public static MvcHtmlString XDateTimeFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
       Expression<Func<TModel, TProperty>> expression,
        UIValidation valid,
        object htmlAttribute)
    {
        ModelMetadata metadata = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, htmlHelper.ViewData);
        return XDateTimeFor(htmlHelper,expression, false, valid, htmlAttribute);
    }

    public static MvcHtmlString XDateTimeFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
       Expression<Func<TModel, TProperty>> expression,
        bool IsTime,
        UIValidation valid, 
        object htmlAttribute)
    {
        ModelMetadata metadata = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, htmlHelper.ViewData);
        return htmlHelper.XDateTime(metadata.GetDisplayName(), metadata.Model,IsTime, valid, htmlAttribute);
    }
    #endregion

    #region Checkbox
    /// <summary>
    /// 复选框控件
    /// </summary>
    /// <param name="htmlHelper"></param>
    /// <param name="name"></param>
    /// <param name="value"></param>
    /// <param name="text"></param>
    /// <param name="htmlAttribute"></param>
    /// <param name="IsChecked">是否必选</param>
    /// <returns></returns>
    public static MvcHtmlString XCheckBox(this HtmlHelper htmlHelper, string name,object value, string text, object htmlAttribute, bool IsChecked, bool dataInt = false)
    {
        StringBuilder html = new StringBuilder();
        var className = "form-group";
      
        string msgError = "";
        IDictionary<string, object> htmlAttributes = htmlAttribute != null ? (IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttribute) : null;
        if (IsPost && IsError(htmlHelper, name, out msgError))
        {
            className += ValidationErrorCssClassName;
        }
        if (value != null && value.ToString().ToLower() == "on")
        {
            value = "true";
        }
        html.Append("\r\n<div id='div_" + name + "' class='" + className + " layout-edit-from-checkbox'>");
        html.Append("\r\n<label class='col-sm-2 control-label'>" + text + "：\r\n</label>");
        html.Append("\r\n<div class='col-sm-10 controls '>");
        html.Append("\r\n<input type='hidden'  name='" + name + "' id='" + name + "' ");
        if (dataInt)
        {
            html.Append("  value='" + (value != null && !string.IsNullOrEmpty(value.ToString())  ? value : "0") + "'/> \r\n ");
        }
        else
        {
            html.Append("  value='" + (value != null && !string.IsNullOrEmpty(value.ToString()) && Convert.ToBoolean(value) ? "true" : "false") + "'/> \r\n ");
        }
       
        
      
        html.Append("\r\n<input class='checkboxinput'  id='chk_"
            + name + "'  data-val='"+((value != null && !string.IsNullOrEmpty(value.ToString()))?value.ToString().ToLower():"false")+"'  type='checkbox' ");
        html.Append("  value='" + (value != null && !string.IsNullOrEmpty(value.ToString()) ? value : "false") + "' ");
        if ((value != null && !string.IsNullOrEmpty(value.ToString())) && (value.ToString().ToLower().Equals("1") || value.ToString().ToLower().Equals("true")))
        {
            html.Append(" checked ");
        }
        if (htmlAttributes["disabled"] != null && htmlAttributes["disabled"].ToString().ToLower()=="true")
        {
            html.Append(" disabled='" + htmlAttributes["disabled"].ToString() + "'");
 
        }
        if (IsChecked)
        {
            UIValidation valid = new UIValidation();
            valid.vaildKey = "IsChecked";
            html.Append(valid.ToJson());
        }
         
        html.Append(TagAttribute(htmlAttributes));
        html.Append("/><p class='text-danger help-block'>" + msgError + "</p></div>");
        html.Append("\r\n</div>");
        return MvcHtmlString.Create(html.ToString());
    }
    public static MvcHtmlString XCheckBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
     Expression<Func<TModel, TProperty>> expression,
      string text, object htmlAttribute, bool IsChecked,bool dataInt=false)
    {
        ModelMetadata metadata = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, htmlHelper.ViewData);
        return htmlHelper.XCheckBox(metadata.GetDisplayName(), metadata.Model, text, htmlAttribute,IsChecked, dataInt);
    }
    #endregion 

    #region radiolist
    /// <summary>
    /// 单选列表控件
    /// </summary>
    /// <param name="htmlHelper"></param>
    /// <param name="name"></param>
    /// <param name="model"></param>
    /// <param name="data"></param>
    /// <param name="valid"></param>
    /// <param name="htmlAttribute"></param>
    /// <returns></returns>
    public static MvcHtmlString XRadioList(this HtmlHelper htmlHelper, string name,object value, object data, UIValidation valid, object htmlAttribute)
    {
        StringBuilder html = new StringBuilder();
       
        var className = "form-group";
        string msgError = "";
        string displayName = "text";
        string displayValue = "id";
        IDictionary<string, object> htmlAttributes = htmlAttribute != null ? (IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttribute) : null;
        if (IsPost && IsError(htmlHelper, name, out msgError))
        {
            className += ValidationErrorCssClassName;
        }
        if (value == null || string.IsNullOrEmpty(value.ToString()))
        {
            value = (valid != null) ? valid.DefaultValue : null;
        }
        if (htmlAttributes != null)
        {
            if (htmlAttributes.Keys.Contains("displayName"))
            {
                displayName = htmlAttributes["displayName"].ToString();
            }
            if (htmlAttributes.Keys.Contains("displayName"))
            {
                displayValue = htmlAttributes["displayValue"].ToString();
            }
        }
        html.Append("\r\n<div id='div_" + name + "' key='" + name + "' class='" + className + " radioboxlist' ");
        html.Append((valid != null ? valid.ToJson() : ""));
        html.Append(" >");
        if (valid != null && !string.IsNullOrEmpty(valid.DisplayName))
        {
            html.Append("<label for='" + name + "' class='col-sm-2 control-label'>");
           
            if (valid.required)
            {
                html.Append("<span class='asteriskField'>*</span>");
            }
            else
            {
                html.Append("<span class='asteriskField'>&nbsp;</span>");
            }
            html.Append(valid.DisplayName+ "：");
            html.Append("</label>");
        }
        html.Append("<div class='col-sm-2 controls '>");
        html.Append(htmlHelper.CreateData(name, data, displayName, displayValue, value.ToString(), false, htmlAttributes,(valid!=null?valid.ValueTranslate:false)));
        html.Append("<p class='text-danger help-block'>" + msgError + "</p></div>");

        html.Append("</div>");
        return MvcHtmlString.Create(html.ToString());
    }

    public static MvcHtmlString XRadioListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
     Expression<Func<TModel, TProperty>> expression,
      object data, UIValidation valid, object htmlAttribute)
    {
        ModelMetadata metadata = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, htmlHelper.ViewData);
        return htmlHelper.XRadioList(metadata.GetDisplayName(), metadata.Model, data, valid, htmlAttribute);
    }
    #endregion

    #region checkboxlist
    /// <summary>
    /// 复选框控件
    /// </summary>
    /// <param name="htmlHelper"></param>
    /// <param name="name"></param>
    /// <param name="model"></param>
    /// <param name="keyValue"></param>
    /// <param name="valid"></param>
    /// <param name="htmlAttribute"></param>
    /// <returns></returns>
    public static MvcHtmlString XCheckboxList(this HtmlHelper htmlHelper, string name, object value, object data, UIValidation valid, object htmlAttribute)
    {
        StringBuilder html = new StringBuilder();
        var className = "form-group";
        string msgError = "";
       
        string displayName = "text";
        string displayValue = "id";
        IDictionary<string, object> htmlAttributes = htmlAttribute != null ? (IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttribute) : null;
        if (IsPost && IsError(htmlHelper, name, out msgError))
        {
            className += ValidationErrorCssClassName;
        }
        if (value==null||string.IsNullOrEmpty(value.ToString()))
        {
            value = (valid != null) ? valid.DefaultValue : "";
        }
        string[] arrValue = value.ToString().Split(new char[] { ',', ';' });
        if (htmlAttributes != null)
        {
            if (htmlAttributes.Keys.Contains("displayName"))
            {
                displayName = htmlAttributes["displayName"].ToString();
            }
            if (htmlAttributes.Keys.Contains("displayValue"))
            {
                displayValue = htmlAttributes["displayValue"].ToString();
            }
        }
        html.Append("\r\n<div id='div_" + name + "' key='"+name+"' class='" + className + " checkboxlist' ");
        html.Append((valid != null ? valid.ToJson() : ""));
        html.Append(" >");
        if (valid != null && !string.IsNullOrEmpty(valid.DisplayName))
        {
            html.Append("<label for='" + name + "' class='control-label'>");
            html.Append(valid.DisplayName);
            if (valid.required)
            {
                html.Append("\r\n<span class='asteriskField'>*</span>");
            }
            else
            {
                html.Append("<span class='asteriskField'>&nbsp;</span>");
            }
            html.Append("</label>");
        }
        html.Append("\r\n<div class='controls '>");

        html.Append(CreateData(htmlHelper,name, data, displayName, displayValue, value.ToString(), true, htmlAttributes,(valid!=null?valid.ValueTranslate:false)));

        html.Append("\r\n<p class='text-danger help-block'>" + msgError + "</p></div>");

        html.Append("</div>");
        return MvcHtmlString.Create(html.ToString());
    }

    public static MvcHtmlString XCheckboxListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
    Expression<Func<TModel, TProperty>> expression,
     object data, UIValidation valid, object htmlAttribute)
    {
        ModelMetadata metadata = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, htmlHelper.ViewData);
        return htmlHelper.XCheckboxList(metadata.GetDisplayName(), metadata.Model, data, valid, htmlAttribute);
    }
    #endregion

    #region Money
    /// <summary>
    /// 金额
    /// </summary>
    /// <param name="htmlHelper"></param>
    /// <param name="name"></param>
    /// <param name="model"></param>
    /// <param name="valid"></param>
    /// <param name="htmlAttribute"></param>
    /// <returns></returns>
    public static MvcHtmlString XMoney(this HtmlHelper htmlHelper, string name,UIValidation valid, object htmlAttribute)
    {
        if (valid == null)
        {
            valid = new UIValidation();
           
        }
        valid.vaildKey = "money";
        if (htmlAttribute == null)
        {
            htmlAttribute = new { AutoComplete = "off" };
        }
        return htmlHelper.XMoney(name, "", valid, htmlAttribute);
    }
    public static MvcHtmlString XMoney(this HtmlHelper htmlHelper, string name,string value, UIValidation valid, object htmlAttribute)
    {
        if (valid == null)
        {
            valid = new UIValidation();

        }
        valid.vaildKey = "money";
        if (htmlAttribute == null)
        {
            htmlAttribute = new { AutoComplete = "off" };
        }
        return XTextBox(htmlHelper, name, xInputType.text, value, "<span class='icon-money'></span>", valid, htmlAttribute);
    }
    public static MvcHtmlString XMoneyFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
        Expression<Func<TModel, TProperty>> expression, UIValidation vaild)
    {
        if (vaild == null)
        {
            vaild = new UIValidation();
        }
        vaild.vaildKey = "money";
        return XTextBoxFor(htmlHelper, expression, "<span class='icon-money'></span>", vaild);
    }
    public static MvcHtmlString XMoneyFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
       Expression<Func<TModel, TProperty>> expression, UIValidation vaild, object htmlAttribute)
    {
        if (vaild == null)
        {
            vaild = new UIValidation();
        }
        if (htmlAttribute == null)
        {
            htmlAttribute = new { AutoComplete = "off" };
        }
        vaild.vaildKey = "money";
        return XTextBoxFor(htmlHelper, expression, "<span class='icon-money'></span>", vaild, htmlAttribute);
    }
    #endregion

    #region Email
    public static MvcHtmlString XEmailTextBox(this HtmlHelper htmlHelper, string name, UIValidation vaild, object htmlAttribute)
    {
        if (vaild == null)
        {
            vaild = new UIValidation();
        }
        vaild.vaildKey = "email";
        return XTextBox(htmlHelper, name, xInputType.email, "<span class='icon-envelope'></span>", vaild, htmlAttribute);
    }
    public static MvcHtmlString XEmailTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
        Expression<Func<TModel, TProperty>> expression, UIValidation vaild, object htmlAttribute)
    {
        if (vaild == null)
        {
            vaild = new UIValidation();
        }
        vaild.vaildKey = "email";
        return XTextBoxFor(htmlHelper, expression, xInputType.email, "<span class='icon-envelope'></span>", vaild, htmlAttribute);
    }
    #endregion

    #region XLinkTextBox
    public static MvcHtmlString XLinkTextBox(this HtmlHelper htmlHelper, string name, UIValidation vaild, object htmlAttribute)
    {
        if (vaild == null)
        {
            vaild = new UIValidation();
        }
        //vaild.vaildKey = "url";
        return XTextBox(htmlHelper, name, xInputType.text, "<span class='icon-link icon-margin-right'></span>", vaild, htmlAttribute);
    }
    public static MvcHtmlString XLinkTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
        Expression<Func<TModel, TProperty>> expression, UIValidation vaild)
    {
        if (vaild == null)
        {
            vaild = new UIValidation();
        }
        //vaild.vaildKey = "url";
        return XTextBoxFor(htmlHelper, expression, "<span class='icon-link icon-margin-right'></span>", vaild);
    }
    #endregion

    #region icon-picture
    public static MvcHtmlString XImage(this HtmlHelper htmlHelper, string name,UIValidation vaild, object htmlAttribute)
    {
        if (vaild == null)
        {
            vaild = new UIValidation();
        }
        //vaild.vaildKey = "url";
        return XTextBox(htmlHelper, name, xInputType.text, "<span class='icon-picture'></span>", vaild, htmlAttribute);
    }
    public static MvcHtmlString XImageFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
        Expression<Func<TModel, TProperty>> expression, UIValidation vaild)
    {
        if (vaild == null)
        {
            vaild = new UIValidation();
        }
        //vaild.vaildKey = "url";
        return XTextBoxFor(htmlHelper, expression, "<span class='icon-picture'></span>", vaild);
    }
    #endregion

    #region order 
    /// <summary>
    /// 排序
    /// </summary>
    /// <param name="htmlHelper"></param>
    /// <param name="name"></param>
    /// <param name="model"></param>
    /// <param name="valid"></param>
    /// <param name="htmlAttribute"></param>
    /// <returns></returns>
    public static MvcHtmlString XOrder(this HtmlHelper htmlHelper, string name,object model, UIValidation valid, object htmlAttribute)
    {
        if (valid == null)
        {
            valid = new UIValidation();

        }
        valid.vaildKey = "IsNumber";
        if (htmlAttribute == null)
        {
            htmlAttribute = new { AutoComplete = "off" };
        }

        return XTextBox(htmlHelper, name, xInputType.number, model, "<span class='icon-list-ol'></span>", valid, htmlAttribute);
    }

    public static MvcHtmlString XOrderFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
      Expression<Func<TModel, TProperty>> expression, UIValidation vaild, object htmlAttribute)
    {
        if (vaild == null)
        {
            vaild = new UIValidation();
        }
        if (htmlAttribute == null)
        {
            htmlAttribute = new { AutoComplete = "off" };
        }
        vaild.vaildKey = "IsNumber";
        return XTextBoxFor(htmlHelper, expression, xInputType.number, "<span class='icon-list-ol'></span>", vaild, htmlAttribute);
    }
    #endregion

    #region Edit submit
    public static MvcHtmlString XButtonList(this HtmlHelper htmlHelper, Array buttonlist)
    {
        return htmlHelper.XButtonList(buttonlist,"");
    }
   
    /// <summary>
    /// 按钮绑定
    /// </summary>
    /// <param name="htmlHelper"></param>
    /// <param name="buttonlist"></param>
    /// <param name="ControlName">控制器</param>
    /// <returns></returns>
    public static MvcHtmlString XButtonList(this HtmlHelper htmlHelper, Array buttonlist, string ControlName)
    {
        StringBuilder html = new StringBuilder();
        XButton defaultbtn = null;
        for (int i = 0; i <  buttonlist.Length; i++)
        {
            XButton btn = buttonlist.GetValue(i) as XButton;
            if (btn.hide)
                continue;
             defaultbtn = btn;
             break;
        }


        html.Append("<div class='form-group'>");
        html.Append("<div class='col-sm-12 form-buttons'>");
        //html.Append("\r\n<div class='btn-group clearfix show-xs save-group col-xs-12'>"); 
        //if (defaultbtn != null)
        //{
        //    if (!string.IsNullOrEmpty(ControlName))
        //    {
        //        defaultbtn.ControlName = ControlName;
        //    }
        //    if (defaultbtn.IsFunction())
        //    {
        //        html.Append("\r\n<button type='" + ((defaultbtn.xButtonType == XButtonType.Submit) ? "submit" : "button") + "' " + (!defaultbtn.disabled ? "disabled=\"disabled\"" : "") + " class='default btn btn-primary col-xs-10'   " + ((defaultbtn.xButtonType == XButtonType.Back) ? " onclick='window.history.go(-1);' " : "") + " name='" + defaultbtn.Id + "' data-loading-text='" + defaultbtn.LoadingText + "'  " + (defaultbtn.hide ? "style=\"display:none\"" : "") + " " + CreateEvent(defaultbtn, defaultbtn.ListEvent) + "> <i class='" + defaultbtn.Icon + "'></i>" + WebCommonHelper.GetResource(defaultbtn.DisplayName) + "</button>");
        //    }
        //}
        ////if (defaultbtn!=null&&buttonlist != null && buttonlist.Length > 1)
        ////{
        ////    // " + (((XButton)buttonlist.GetValue(0)).hide ? "style=\"display:none\"" : "") + "
        ////    html.Append("\r\n<button type='button' " + (!((XButton)buttonlist.GetValue(0)).disabled ? "disabled=\"disabled\"" : "") + "  class='more btn btn-primary col-xs-2' data-toggle='collapse' data-target='.nav-collapse.more-btns'  ><i class='icon-ellipsis-vertical'></i></button>");
        ////}
        //html.Append("\r\n</div>");
        if (defaultbtn != null)
        {
            if (defaultbtn.IsFunction())
            {
                html.Append("\r\n<button type='" + ((defaultbtn.xButtonType == XButtonType.Submit) ? "submit" : "button") + "'  " + (!defaultbtn.disabled ? "disabled=\"disabled\"" : "") + "  class='default btn btn-primary '   " + ((defaultbtn.xButtonType == XButtonType.Back) ? " onclick='window.history.go(-1);' " : "") + " name='" + defaultbtn.Id + "' data-loading-text='" + defaultbtn.LoadingText + "'  " + (defaultbtn.hide ? "style=\"display:none\"" : "") + " " + CreateEvent(defaultbtn, defaultbtn.ListEvent) + "><i class='" + defaultbtn.Icon + "'></i>" + defaultbtn.DisplayName + "</button>");
            }
        }
      
        if (buttonlist != null && buttonlist.Length > 1)
        {
           // html.Append("\r\n<div class='nav-collapse collapse more-btns'>");
            foreach (object item in buttonlist)
            {
                XButton btn = item as XButton;
                if (defaultbtn!=null&&defaultbtn.Id == btn.Id)
                    continue;
                if (!string.IsNullOrEmpty(ControlName))
                {
                    btn.ControlName = ControlName;
                }
                if (!btn.IsFunction())
                {
                    continue;
                }
               
                html.Append("\r\n<button type='" + ((btn.xButtonType == XButtonType.Submit) ? "submit" : "button") + "' " + (!btn.disabled ? "disabled=\"disabled\"" : "") + "  " + ((btn.xButtonType == XButtonType.Back) ? " onclick='window.history.go(-1);' " : "") + "' class='btn btn-default' name='" + btn.Id + "'  id='" + btn.Id + "'  " + (btn.hide ? "style=\"display:none\"" : "") + "");
                html.Append(CreateEvent(btn, btn.ListEvent));
                if (!string.IsNullOrEmpty(btn.LoadingText))
                {
                    html.Append("  data-loading-text='" + btn.LoadingText + "'");
                }
                html.Append(">");
                if (!string.IsNullOrEmpty(btn.Icon))
                {
                    html.Append("<i class='"+btn.Icon+"'></i>");
                }
                html.Append (btn.DisplayName);
                html.Append("</button>");
            }

           // html.Append("\r\n</div>");
        }
        html.Append("</div>");
        html.Append("</div>");
        return MvcHtmlString.Create(html.ToString());
    }
    /// <summary>
    /// 创建事件
    /// </summary>
    /// <param name="listEvent"></param>
    /// <returns></returns>
    static string CreateEvent(XButton obj,Array listEvent)
    {
        StringBuilder strEvent = new StringBuilder();
        if (!string.IsNullOrEmpty(obj.onClick))
        {
            strEvent.Append(" onclick=\"" + obj.onClick + "\" ");
        }
        if (listEvent != null && listEvent.Length > 0)
        {
            foreach (object item in listEvent)
            {
                XBtnEvent btn = item as XBtnEvent;
                strEvent.Append(" "+btn.EventName+"=\""+btn.Function+"\"");
            }
        }
        return strEvent.ToString();
    }
    #endregion

    #region btn-group
    /// <summary>
    /// 新增按钮
    /// </summary>
    /// <param name="htmlhelper"></param>
    /// <param name="gridButton"></param>
    /// <param name="htmlAttribute"></param>
    /// <returns></returns>
    public static MvcHtmlString XActionLink(this HtmlHelper htmlhelper, XGridButton gridButton, object htmlAttribute)
    {
        StringBuilder html = new StringBuilder();
        if (gridButton.Hide || !gridButton.IsFunction())
        {
            return MvcHtmlString.Create(html.ToString());
        }
       
        IDictionary<string, object> htmlAttributes = htmlAttribute != null ? (IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttribute) : null;
        if (htmlAttributes == null || htmlAttributes["hide"] == null || !htmlAttributes["hide"].ToString().Equals("true", StringComparison.CurrentCultureIgnoreCase))
        {
            html.Append("<a ");
            if (string.IsNullOrEmpty(gridButton.Url))
            {
                html.Append(" href='javascript://' ");
            }
            else
            {
                html.Append(" href='" + gridButton.Url + "' ");
            }
            if (!string.IsNullOrEmpty(gridButton.onclick))
            {
                html.Append(" onclick='" + gridButton.onclick + "' ");
            }
            html.Append(TagAttribute(htmlAttributes));
            html.Append(" class='btn btn-primary'><i class='" + gridButton.Icon + " icon-margin-right'></i>" + gridButton.BtnName + "</a>");
        }
        return MvcHtmlString.Create(html.ToString());
    }
    /// <summary>
    /// 生成列表按钮
    /// </summary>
    /// <param name="htmlHelper"></param>
    /// <param name="btn"></param>
    /// <param name="htmlAttribute"></param>
    /// <returns></returns>
    public static MvcHtmlString XButtonGroup(this HtmlHelper htmlHelper,XGridButton btn, object htmlAttribute)
    {
        StringBuilder html = new StringBuilder();
        if (btn.Hide || !btn.IsFunction())
        {
            return MvcHtmlString.Create(html.ToString());
        }
       
        IDictionary<string, object> htmlAttributes = htmlAttribute != null ? (IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttribute) : null;

        html.Append("<div class=\"btn-group layout-btns\">");
            html.Append("<a ");
            if (string.IsNullOrEmpty(btn.Url))
            {
                html.Append(" href='javascript://' ");
            }
            else
            {
                html.Append(" href='"+btn.Url+"' ");
            }
            if (!string.IsNullOrEmpty(btn.onclick))
            {
                html.Append(" onclick='"+btn.onclick+"' ");
            }
            if (!string.IsNullOrEmpty(btn.DeleteUrl))
            {
                html.Append(" data-del='" + btn.DeleteUrl + "' ");
            }
            if (!string.IsNullOrEmpty(btn.GridName))
            {
                html.Append(" data-grid='" + btn.GridName + "' ");
            }
            html.Append(TagAttribute(htmlAttributes));
            html.Append(" class='btn btn-default btn-sm'><i class='" + btn.Icon + " icon-margin-right'></i>&nbsp;" + btn.BtnName + "</a>");
            html.Append("</div>");
  
        return MvcHtmlString.Create(html.ToString());
    }
    #endregion

    #region file
    //public static MvcHtmlString UploadFiles(this HtmlHelper htmlhelper, string name, string Text, string upUrl,object htmlAttribute)
    //{
    //    return UploadFiles(htmlhelper,name,Text,upUrl,null,null,htmlAttribute);
    //}
    ///// <summary>
    ///// accept 附件类型 multiple是否同时上传多文件
    ///// </summary>
    ///// <param name="htmlhelper"></param>
    ///// <param name="name"></param>
    ///// <param name="Text"></param>
    ///// <param name="upUrl"></param>
    ///// <param name="valid"></param>
    ///// <param name="files"></param>
    ///// <param name="htmlAttribute"></param>
    ///// <returns></returns>
    //public static MvcHtmlString UploadFiles(this HtmlHelper htmlhelper, string name, string Text, string upUrl, UIValidation valid, object files, object htmlAttribute)
    //{
    //    StringBuilder html = new StringBuilder();
    //    var className = "form-group";
     
    //    //List<ViewDataUploadFilesResult> files
    //    string msgError = "";
    //    //if (IsError(htmlHelper, name, out msgError, out value))
    //    //{
    //    //    className += ValidationErrorCssClassName;
    //    //}
    //    IDictionary<string, object> htmlAttributes = htmlAttribute != null ? (IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttribute) : null;
    //    html.Append("<div class='col-sm-12 '>");
    //    html.Append("\r\n<div id='div_" + name + "' class='" + className + "'>");
    //    //html.Append("<label for='" + name + "' class='control-label'>");
    //    //html.Append((valid != null ? valid.DisplayName : Text));
    //    //if (valid!=null&&valid.required)
    //    //{
    //    //    html.Append("<span class='asteriskField'>*</span>");
    //    //}
    //    //html.Append("</label>");
    //    html.Append("<div class='controls '>");
    //    html.Append("\r\n<div class='input-group text-field'  style='" + (valid != null && valid.disabled ? "display:none;" : "") + "margin-left:20px;'>");
    //    html.Append("\r\n<span class='btn btn-success fileinput-button' >");
    //    html.Append("<i class='glyphicon glyphicon-plus icon-margin-right'></i>");
    //    html.Append("<span>" + (!string.IsNullOrEmpty(Text) ? Text : "Select files...") + "</span>");//
    //    html.Append("<input id='" + name + "' type='file' name='" + name + "'  accept='" + ConfigurationHelper.GetSetting("upLoadfileType") + "'  data-url='" + upUrl + "' ");
    //    //html.Append((valid != null ? valid.ToJson() : ""));
    //    html.Append(TagAttribute(htmlAttributes));
    //    html.Append("/>");
    //    string RequestTaskId = "";
    //    if (htmlAttributes.ContainsKey("RequestTaskId") && htmlAttributes["RequestTaskId"] != null)
    //    {
    //        RequestTaskId = htmlAttributes["RequestTaskId"].ToString();
    //    }
    //    string StepName = "";
    //    if (htmlAttributes.ContainsKey("StepName") && htmlAttributes["StepName"] != null)
    //    {
    //        StepName = htmlAttributes["StepName"].ToString();
    //    }
    //    html.Append("</span></div>");
    //    html.Append("\r\n<table class='table table-hover table-striped'>");
    //    html.Append("\r\n<tbody class='files'>");
    //    if (files!=null)
    //    {
    //        if (files is List<ViewDataUploadFilesResult>)
    //        {
    //            foreach (ViewDataUploadFilesResult item in files as List<ViewDataUploadFilesResult>)
    //            {
    //                html.Append(ViewFiles(item, valid, false, htmlAttributes, RequestTaskId, StepName));
    //            }
    //        }
    //        else if (files is ViewDataUploadFilesResult)
    //        {
    //            html.Append(ViewFiles(files as ViewDataUploadFilesResult, valid, true, htmlAttributes, RequestTaskId, StepName));
    //        }
    //    }
    //    html.Append("\r\n</tbody>");
    //    html.Append("\r\n</table>");
    //    html.Append("\r\n<p class='text-danger help-block'>" + msgError + "</p></div>");
    //    html.Append("</div>");
    //    html.Append("</div>");
    //    return MvcHtmlString.Create(html.ToString());
    //}
  
    ///// <summary>
    ///// 文件显示
    ///// </summary>
    ///// <param name="item"></param>
    ///// <param name="valid"></param>
    ///// <returns></returns>
    //private static string ViewFiles(ViewDataUploadFilesResult item, UIValidation valid, bool IsOneFile, IDictionary<string, object> htmlAttributes, string RequestTaskId,string StepName)
    //{
    //    StringBuilder html = new StringBuilder();
    //    html.Append("\r\n<tr class='template-upload fade in'>");
    //    Regex reg= new Regex(".+\\.(jpg|png|bmp|jpeg|gif)$");
    //    string delCallbak = "";
    //    if (htmlAttributes != null && htmlAttributes.Count > 0 && htmlAttributes["delCallbak"]!=null)
    //    {
    //        delCallbak = htmlAttributes["delCallbak"].ToString();
    //    }
    
    //    delCallbak = !string.IsNullOrEmpty(delCallbak) ? delCallbak+"($(this),'" + item.delete_url + "','" + item.path + "');" : "return jUpload.Delete($(this),'" + item.delete_url + "','" + item.path + "');";
    //    if (IsOneFile && item != null && !string.IsNullOrEmpty(item.name) && reg.IsMatch(item.name))
    //    {//单图片文件展示
    //        html.Append("\r\n<td>");
    //        html.Append("\r\n<p class='name'><a href='" + item.url + "' target='_blank'><img src='" + item.url + "' border=0 style='width:40px;height:40px;' /></a></p>");
    //        html.Append("\r\n<strong class='error text-danger'></strong>");
    //        html.Append("\r\n</td>");
    //        html.Append("\r\n<td>");
    //        // && (string.IsNullOrEmpty(RequestTaskId) || (!string.IsNullOrEmpty(RequestTaskId) && RequestTaskId == item.taskId))
    //        if (!string.IsNullOrEmpty(item.delete_url))//是否显示删除按钮
    //        {
    //            if (valid != null && !valid.disabled)
    //            {
    //                html.Append("\r\n<button type='button' class='btn btn-warning cancel' onclick=\"" + delCallbak + "\"><i class='icon-remove-circle'></i><span>Cancel</span></button>");
    //            }
    //        }
    //        html.Append("\r\n</td>");
    //    }
    //    else
    //    {
    //        html.Append("\r\n<td>" + item.StepName + "</td>");
    //        html.Append("\r\n<td>");
    //        html.Append("\r\n<p class='name'><a href='" + item.url + "' target='_blank'>" + item.name + "</a></p>");
    //        html.Append("\r\n<strong class='error text-danger'></strong>");
    //        html.Append("\r\n</td>");
    //        html.Append("\r\n<td>");
    //        html.Append("\r\n<p class='size'>" + Math.Round(Convert.ToDouble(item.size) / 1024, 2) + "KB</p>");
    //        //if (!valid.disabled)
    //        //{
    //        //  html.Append("\r\n<div class='progress progress-striped active' role='progressbar' aria-valuemin='0' aria-valuemax='100' aria-valuenow='0'><div class='progress-bar progress-bar-success' style='width:100%;'></div></div>");
    //        //}
    //        html.Append("\r\n</td>");
    //        html.Append("\r\n<td>");
    //        html.Append("\r\n" + item.createUser);
    //        html.Append("\r\n</td>");
    //        html.Append("\r\n<td>");
    //        html.Append("\r\n" + item.createDate);
    //        html.Append("\r\n</td>");

    //        html.Append("\r\n<td>");

    //        //&& (string.IsNullOrEmpty(RequestTaskId) || (!string.IsNullOrEmpty(RequestTaskId) && RequestTaskId == item.taskId))
    //        if (valid != null && !valid.disabled )
    //        {
    //            if (string.IsNullOrEmpty(StepName) || (!string.IsNullOrEmpty(StepName) && StepName.Equals(item.oldStepName, StringComparison.CurrentCultureIgnoreCase)))
    //            {
    //                html.Append("\r\n<button type='button' class='btn btn-warning cancel' onclick=\"" + delCallbak + "\"><i class='icon-remove-circle'></i><span>Cancel</span></button>");
    //            }
               
    //        }
    //        html.Append("\r\n</td>");
    //        html.Append("\r\n</tr>");
    //    }
    //    return html.ToString();
    //}
    #endregion

    #region linkbutton
    /// <summary>
    /// 链接控件
    /// </summary>
    /// <param name="htmlhelper"></param>
    /// <param name="Text"></param>
    /// <param name="iconclass"></param>
    /// <param name="url">跳转的url</param>
    /// <returns></returns>
    public static MvcHtmlString XActionLink(this HtmlHelper htmlhelper, string Text, string iconclass, string url)
    {
        return XActionLink(htmlhelper, Text, iconclass, url,false, null);
    }
 
    /// <summary>
    /// 链接控件
    /// </summary>
    /// <param name="htmlhelper"></param>
    /// <param name="Text"></param>
    /// <param name="iconclass"></param>
    /// <param name="url"></param>
    /// <param name="IsButton">是否为button按钮，如果是，则不会跳转</param>
    /// <param name="htmlAttribute"></param>
    /// <returns></returns>
    public static MvcHtmlString XActionLink(this HtmlHelper htmlhelper, string Text, string iconclass, string url,bool IsButton, object htmlAttribute)
    {
        return XLinkButton(htmlhelper, Text, "btn btn-primary",iconclass,url,IsButton,htmlAttribute);
    }

    public static MvcHtmlString XToggleLink(this HtmlHelper htmlhelper,  string iconclass, string url)
    {
        return htmlhelper.XToggleLink((!string.IsNullOrEmpty(iconclass) ? iconclass : "icon-plus icon-margin-right") + " icon-white", url, false, null);
    }
    public static MvcHtmlString XToggleLink(this HtmlHelper htmlhelper,string iconclass, string url, bool IsButton, object htmlAttribute)
    {
        return XLinkButton(htmlhelper, "", "navbar-toggle pull-right", (!string.IsNullOrEmpty(iconclass) ? iconclass : "icon-plus icon-margin-right") + " icon-white", url, IsButton, htmlAttribute);
    }
    
    /// <summary>
    /// 链接控件
    /// </summary>
    /// <param name="htmlhelper"></param>
    /// <param name="Text"></param>
    /// <param name="iconclass"></param>
    /// <param name="url"></param>
    /// <param name="IsButton">是否为button按钮，如果是，则不会跳转</param>
    /// <param name="htmlAttribute"></param>
    /// <returns></returns>
    public static MvcHtmlString XLinkButton(this HtmlHelper htmlhelper, string Text,string className, string iconclass, string url, bool IsButton, object htmlAttribute)
    {
        StringBuilder html = new StringBuilder();
        
        IDictionary<string, object> htmlAttributes = htmlAttribute != null ? (IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttribute) : null;
        if (htmlAttributes == null || htmlAttributes["hide"] == null || !htmlAttributes["hide"].ToString().Equals("true", StringComparison.CurrentCultureIgnoreCase))
        {
            html.Append("<a href='" + (IsButton ? "javascript://" : url) + "'");
            html.Append(TagAttribute(htmlAttributes));
            html.Append(" class='" + className + "'><i class='" + iconclass + "'></i>" + Text + "</a>");
        }
        return MvcHtmlString.Create(html.ToString());
    }
   
    #endregion 

    private static string TagAttribute(IDictionary<string, object> htmlAttributes)
    {
        StringBuilder html = new StringBuilder();
        if (htmlAttributes != null && htmlAttributes.Count > 0)
        {
            foreach (var key in htmlAttributes.Keys)
            {
                if (key.Equals("class") 
                    || key.Equals("displayValue") 
                    || key.Equals("displayName") 
                    || key.Equals("split")
                    || key.Equals("delCallbak"))
                    continue;
                else if (key.Equals("read_only") || key.Equals("read-only"))
                {
                    html.Append(" readonly=\"" + htmlAttributes[key] + "\"");
                }
                else if (key.Equals("disabled"))
                {
                    if (!string.IsNullOrEmpty(htmlAttributes[key].ToString()) && htmlAttributes[key].ToString().ToLower()=="true")
                    {
                        html.Append(" " + key + "=\"" + htmlAttributes[key] + "\"");
                    }
                }
                else
                {
                    html.Append(" " + key + "=\"" + htmlAttributes[key] + "\"");
                }
            }
        }
        return html.ToString();
    }

    /// <summary>
    /// 判断控件是否有错并获取值
    /// </summary>
    /// <param name="htmlHelper"></param>
    /// <param name="fieldName"></param>
    /// <param name="error"></param>
    /// <param name="value"></param>
    /// <param name="IsExpression"></param>
    /// <returns></returns>
    public static bool IsError(this HtmlHelper htmlHelper, string fieldName, out string error)
    {
        error = "";
        string fullHtmlFieldName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(fieldName);
        ModelState state;
        var flag = htmlHelper.ViewData.ModelState.TryGetValue(fullHtmlFieldName, out state) && (state.Errors.Count > 0);
        if (state != null && state.Value!=null)
        {
            //if (!IsExpression)
            //{
            //    value = state.Value.AttemptedValue;
            //}
        }
        if (flag)
        {
            error = state.Errors[0].ErrorMessage;
        }
        return flag;
    }
    public static object GetModelStateValue(HtmlHelper htmlHelper, string fieldName)
    {
        object value = null;
        string fullHtmlFieldName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(fieldName);
        ModelState state;
        var flag = htmlHelper.ViewData.ModelState.TryGetValue(fullHtmlFieldName, out state) && (state.Errors.Count > 0);
        if (state != null && state.Value != null)
        {
            value = state.Value.AttemptedValue;
        }
        return value;
    }
  

    public static MvcHtmlString CreateGanderRadioButton(this System.Web.Mvc.HtmlHelper html)
    {
        StringBuilder str = new StringBuilder();
        str.Append("<input type='radio' value=1 name='gander'>男");
        str.Append("<input type='radio' value=0 name='gander'>女");
        return MvcHtmlString.Create(str.ToString());

    }
     #region search
    /// <summary>
    /// 搜索条件中的下拉选项
    /// </summary>
    /// <param name="htmlHelper"></param>
    /// <param name="gridId">对应的gridId</param>
    /// <param name="fieldName">对应的paramName</param>
    /// <param name="Data">数据列表</param>
    /// <param name="IsTranslate">是否需要翻译</param>
    /// <param name="IsDBNull">是否为空</param>
    /// <param name="displayName">显示字段名</param>
    /// <param name="displayValue">对应值</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns></returns>
    public static MvcHtmlString SdropDownList(this HtmlHelper htmlHelper, string gridId, 
        string fieldName, object data, bool IsTranslate, bool IsDBNull, 
        string displayName, string displayValue,string defaultValue)
    {
        StringBuilder strHtml = new StringBuilder();
        strHtml.Append("<ul class=\"dropdown-menu\" role=\"menu\" id=\"drp" + fieldName + "\" aria-labelledby=\"drop-filter\" data-grid=\"" + gridId + "\" data-name=\"" + fieldName + "\">");
        if (IsDBNull)
        {
            strHtml.Append("<li data-value=''>");
            strHtml.Append("<a >所有</a>");
            strHtml.Append(" </li>");
        }
        if (data is DataTable)
        {
            DataTable dt = (DataTable)data;
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    if (dt.Columns.Contains(displayName) && item[displayName] != null)
                    {
                        strHtml.Append("<li data-value='" + (item[displayValue] != null ? item[displayValue] : item[displayName]) + "' " + ((((item[displayValue] != null && item[displayValue].Equals(defaultValue)) || (item[displayValue] == null && item[displayName].Equals(defaultValue)))) ? " class='active'" : "") + "> <a><i class=\"text-muted\"></i>" + item[displayName].ToString() + "</a></li>");
                    }
                }
            }
        }

        else if (data is Dictionary<string, string>)
        {
            Dictionary<string, string> dic = (Dictionary<string, string>)data;
            if (dic != null)
            {
                foreach (var key in dic.Keys)
                {
                    string display = "&nbsp;";
                    if (!string.IsNullOrEmpty(dic[key]))
                    {
                        display = dic[key];
                        
                    }
                    strHtml.Append("<li data-value='" + key + "' " + ((key.Equals(defaultValue)) ? " class='active'" : "") + " > <a><i class=\"text-muted\"></i>" + (!string.IsNullOrEmpty(display) ? display : "&nbsp;") + "</a></li>");
                }
            }
        }
        else if (data is Dictionary<string, object>)
        {
            Dictionary<string, object> dic = (Dictionary<string, object>)data;
            if (dic != null)
            {
                foreach (var key in dic.Keys)
                {
                    strHtml.Append("<li data-value='" + key + "' " + (!string.IsNullOrEmpty(defaultValue) && key.Equals(defaultValue) ? " class='active'" : "") + " > <a><i class=\"text-muted\"></i>" + (!string.IsNullOrEmpty(dic[key].ToString()) ? dic[key].ToString() : "&nbsp;") + "</a></li>");
                }
            }
        }
        else if (data is List<string>)
        {
            List<string> dic = (List<string>)data;
            if (dic != null)
            {
                foreach (var key in dic)
                {
                    if (string.IsNullOrEmpty(key))
                    {
                        continue;
                    }
                    strHtml.Append("<li data-value='" + key + "' " + (!string.IsNullOrEmpty(defaultValue) && key.Equals(defaultValue) ? " class='active'" : "") + "> <a><i class=\"text-muted\"></i>" + (!string.IsNullOrEmpty(key) ? key : "&nbsp;") + "</a></li>");
                }
            }
        }
        else if (data != null && data.GetType().IsArray)
        {
            var dic = data as Array;
            if (dic.Length > 0)
            {
                foreach (var item in dic)
                {
                    string value1 = ((item.GetType().GetProperty(displayValue) != null && item.GetType().GetProperty(displayValue).GetValue(item) != null) ? item.GetType().GetProperty(displayValue).GetValue(item).ToString() : "");
                    string display = ((item.GetType().GetProperty(displayValue) != null && item.GetType().GetProperty(displayName).GetValue(item) != null) ? item.GetType().GetProperty(displayName).GetValue(item).ToString() : "");
                    
                    strHtml.Append("<li data-value='" + value1 + "' " + (!string.IsNullOrEmpty(defaultValue) && value1.Equals(defaultValue) ? " class='active'" : "") + "> <a><i class=\"text-muted\"></i>" + (!string.IsNullOrEmpty(display) ? display : "&nbsp;") + "</a></li>");
                }
                //foreach (var key in dic)
                //{
                //    strHtml.Append("<li data-value='" + key + "' " + (!string.IsNullOrEmpty(defaultValue) && key.Equals(value) ? " class='active'" : "") + ">" + (!string.IsNullOrEmpty(key) ? key : "&nbsp;") + "</option>");
                //}
            }
        }
        strHtml.Append("</ul>");
        return MvcHtmlString.Create(strHtml.ToString());
    }

    /// <summary>
    /// 查询时日期段选择控件
    /// </summary>
    /// <param name="htmlHelper"></param>
    /// <param name="gridId"></param>
    /// <param name="FormDate"></param>
    /// <param name="EndDate"></param>
    /// <returns></returns>
    public static MvcHtmlString SFTDateTime(this HtmlHelper htmlHelper, string gridId, string fieldName, string FormDate, string EndDate)
    {
        StringBuilder strHtml = new StringBuilder("");
        strHtml.Append("<div class=\"popover bottom\">");
        strHtml.Append(" <div class=\"arrow\"></div>");
        strHtml.Append("<div class=\"popover-content row\">");
        strHtml.Append(" <form method=\"get\" action=\"\" id=\"frm" + fieldName + "\" class=\"clearfix\"  data-grid=\"" + gridId + "\">");
        strHtml.Append("<div class=\"ranges col-sm-3 hide-xs\">");
        strHtml.Append("<fieldset class=\"range_inputs\">");
        strHtml.Append("<h4>选择日期</h4>");
        strHtml.Append("<label for=\"txt" + fieldName + "1\">开始时间</label>");
        strHtml.Append("<input class=\"form-control start_input\" type=\"text\" id=\"txt" + fieldName + "1\" name=\"" + fieldName + "1\" value=\"" + FormDate + "\" />");
        strHtml.Append("<label for=\"txt" + fieldName + "2\">结束时间</label>");
        strHtml.Append("<input class=\"form-control end_input\" type=\"text\" id=\"txt" + fieldName + "2\" name=\"" + fieldName + "2\" value=\"" + EndDate + "\" />");
        strHtml.Append("<button type=\"submit\" class=\"btn btn-success btn-block\">查询</button>");
        strHtml.Append("</fieldset>");
        strHtml.Append("</div>");
        strHtml.Append("<div class=\"col-sm-9\">");
        strHtml.Append("<div class=\"calendar date-start col-sm-6\" data-date=\"\"></div>");
        strHtml.Append("<div class=\"calendar date-end col-sm-6\" data-date=\"\"></div>");
        strHtml.Append("</div>");
        strHtml.Append(" <div class=\"col-xs-12\">");
        strHtml.Append("<button type=\"submit\" class=\"btn btn-success btn-block show-xs\">查询</button>");
        strHtml.Append("</div>");
        strHtml.Append("</form></div></div>");
      
        return new MvcHtmlString(strHtml.ToString());
    }

    #endregion


    /// <summary>
    /// 判断是否是Post
    /// </summary>
    public static bool IsPost
    {
        get
        {
            
            if (System.Web.HttpContext.Current.Request.HttpMethod.Equals("POST", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// 生成radio checkbox list选项
    /// </summary>
    /// <param name="name">名称</param>
    /// <param name="data">数据集合</param>
    /// <param name="displayName">显示名称</param>
    /// <param name="displayValue">绑定值</param>
    /// <param name="value">值</param>
    /// <param name="IsCheckBox">是否是checkbox</param>
    /// <param name="htmlAttributes">属性</param>
    /// <param name="ValueTranslate">是否需要转换</param>
    /// <returns></returns>
    static string CreateData(this HtmlHelper htmlHelper, string name, object data, 
        string displayName, string displayValue,
        string value, bool IsCheckBox,
        IDictionary<string, object> htmlAttributes, bool ValueTranslate)
    {
        StringBuilder html = new StringBuilder();
       string [] strArr=new string[]{};
       if (IsCheckBox && !string.IsNullOrEmpty(value))
        {
            strArr = value.Split(new char[]{',','|',';'});
        }
    
        #region 遍历数据
        int i = 0;
        if (data is DataTable)
        {
            DataTable dt = (DataTable)data;
            if (dt != null && dt.Rows.Count > 0)
            {

                foreach (DataRow item in dt.Rows)
                {
                    if (dt.Columns.Contains(displayName) && item[displayName] != null)
                    {
                        string itemValue = item[displayValue] != null ? item[displayValue].ToString() : item[displayName].ToString();
                        html.Append("\r\n<label for='" + name + "_" + i.ToString() + "' class='" + (IsCheckBox?"checkbox":"radio") + "-inline'><input name='" + name + "' type='" + (IsCheckBox ? "checkbox" : "radio") + "' id='" + name + "_" + i.ToString() + "' value='" + itemValue + "' " +
                            (htmlHelper.IsChecked(strArr, itemValue, value, true) ? "checked='checked'" : ""));
                        html.Append(TagAttribute(htmlAttributes));
                        html.Append(" />" +  item[displayName].ToString() + "</label>");
                        i++;
                    }
                }
            }
        }
        else if (data is Dictionary<string, string>)
        {
            Dictionary<string, string> dic = (Dictionary<string, string>)data;
            if (dic != null)
            {

                foreach (var key in dic.Keys)
                {

                    html.Append("\r\n<label for='" + name + "_" + i.ToString() + "' class='" + (IsCheckBox ? "checkbox" : "radio") + "-inline'><input name='" + name + "' type='" + (IsCheckBox ? "checkbox" : "radio") + "' id='" + name + "_" + i.ToString() + "'  value='" + key + "' " + (htmlHelper.IsChecked(strArr, key, value, IsCheckBox) ? "checked='checked'" : ""));
                    html.Append(TagAttribute(htmlAttributes));
                    html.Append(" />" + (!string.IsNullOrEmpty(dic[key]) ? dic[key] : "&nbsp;") + "</label>");
                    i++;
                }
            }
        }
        //else if (data is Dictionary<string, object>)
        //{
        //    Dictionary<string, object> dic = (Dictionary<string, object>)data;
        //    if (dic != null)
        //    {
        //        foreach (var key in dic.Keys)
        //        {
        //            string itemValue=dic[key]!=null?dic
        //            html.Append("\r\n<label for='" + name + "_" + i.ToString() + "' class='checkbox-inline'><input name='" + name + "' type='" + (IsCheckBox ? "checkbox" : "radio") + "' id='" + name + "_" + i.ToString() + "'  value='" + key + "' " + (value != null && key.Equals(value) ? "checked='checked'" : ""));
        //            html.Append(TagAttribute(htmlAttributes));
        //            html.Append(" />" + (!string.IsNullOrEmpty(dic[key].ToString()) ? dic[key].ToString() : "&nbsp;") + "</label>");
        //            i++;
        //        }
        //    }
        //}
        else if (data is Array)
        {
            Array arr = (Array)data;
            if (arr != null)
            {
                foreach (var item in arr)
                {
                    string itemValue = ((item.GetType().GetProperty(displayValue) != null && item.GetType().GetProperty(displayValue).GetValue(item) != null) ? item.GetType().GetProperty(displayValue).GetValue(item).ToString() : "");
                    string display = ((item.GetType().GetProperty(displayValue) != null && item.GetType().GetProperty(displayName).GetValue(item) != null) ? item.GetType().GetProperty(displayName).GetValue(item).ToString() : "");
                    html.Append("\r\n<label for='" + name + "_" + i.ToString() + "' class='" + (IsCheckBox ? "checkbox" : "radio") + "-inline'><input name='" + name + "' type='" + (IsCheckBox ? "checkbox" : "radio") + "' id='" + name + "_" + i.ToString() + "'  value='" + itemValue + "' " + (htmlHelper.IsChecked(strArr, itemValue, value,IsCheckBox) ? "checked='checked'" : ""));
                    html.Append(TagAttribute(htmlAttributes));
                    html.Append(" />" + (!string.IsNullOrEmpty(display) ?  display : "&nbsp;") + "</label>");
                    i++;
                }
            }
        }
        return html.ToString();
        #endregion
    }

    /// <summary>
    /// 判断是否选中，适用于checkbox radio
    /// </summary>
    /// <param name="strArr"></param>
    /// <param name="itemValue"></param>
    /// <param name="value"></param>
    /// <param name="IsCheckBox"></param>
    /// <returns></returns>
    static bool IsChecked(this HtmlHelper htmlHelper, string[] strArr, string itemValue, string value, bool IsCheckBox)
    {
        if(string.IsNullOrEmpty(value))
            return false;
        if (!IsCheckBox)
        {
            return itemValue.Equals(value);
        }
        else
        {
            if (strArr != null && strArr.Length > 0)
            {
                return strArr.Contains<string>(itemValue);
            }
        }
        return false;
    }

    /// <summary>
    /// 判断是否有权限操作
    /// </summary>
    /// <param name="controlName">控制器</param>
    /// <param name="ActionName"></param>
    /// <returns></returns>
    static bool IsBtnFunction(string controlName, string ActionName)
    {
        if (string.IsNullOrEmpty(controlName) || string.IsNullOrEmpty(ActionName))
        {
            return true;
        }
        return true;
       // return WebCommonHelper.PageFunctionDisplay(controlName, ActionName);
    }

   
}
   

