using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Common.Web.Mvc.Html
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

        public static MvcHtmlString DataBindedTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, 
                                                                            Expression<Func<TModel, TProperty>> expression, 
                                                                            IDictionary<string, object> bindingAttributes)
        {
            bindingAttributes.Add("value", ExpressionHelper.GetExpressionText(expression));

            return htmlHelper.TextBoxFor(expression, new Dictionary<string, object>
                                                         {
                                                             {
                                                                 "data-bind", 
                                                                 AttributesCollectionAsString(bindingAttributes)
                                                             }
                                                         });
        }

        private static string AttributesCollectionAsString(IDictionary<string, object> bindingAttributes)
        {
            var attributePairs = from attribute in bindingAttributes 
                                    select string.Format("{0}: {1}", attribute.Key, attribute.Value);

            return string.Join(", ", attributePairs);
        }

        #endregion

        #region TextArea

        public static MvcHtmlString DataBindedTextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return htmlHelper.TextAreaFor(expression, new Dictionary<string, object>
                                                          {
                                                              {
                                                                  "data-bind", 
                                                                  string.Concat("value: ", ExpressionHelper.GetExpressionText(expression))
                                                              }
                                                          });
        }

        #endregion
    }
}
