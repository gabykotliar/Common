using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Common.Web.MVC.Html
{
    public static class DataBindedExtensions
    {
        #region TextBox

        public static MvcHtmlString DataBindedTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return htmlHelper.TextBoxFor(expression, new Dictionary<string, object>
                                                         {
                                                             {
                                                                 "data-bind", 
                                                                 string.Concat("value: ", ExpressionHelper.GetExpressionText(expression))
                                                             }
                                                         });
        }

        #endregion

        #region TextArea

        public static MvcHtmlString DataBindedTextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return htmlHelper.TextAreaFor(expression, new Dictionary<string, object>
                                                          {
                                                              {
                                                                  "data-bind", 
                                                                  string.Concat("text: ", ExpressionHelper.GetExpressionText(expression))
                                                              }
                                                          });
        }

        #endregion
    }
}
