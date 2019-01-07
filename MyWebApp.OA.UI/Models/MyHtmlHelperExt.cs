using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace System.Web.Mvc
{
    public static class MyHtmlHelperExt
    {
        public static string MyLable(this HtmlHelper helper, string txt)
        {
            return string.Format("<span style='color:red'>{0}</span>", txt);
        }

        public static MvcHtmlString MyMvcHtmlLabel(this HtmlHelper helper, string txt)
        {
            var str = string.Format("<span style='color:red'>{0}</span>", txt);
            return MvcHtmlString.Create(str);
        }


        public static HtmlString ShowPageNavigate(this HtmlHelper htmlHelper, int currentPage, int pageSize, int totalCount,int id)
        {
            var redirectTo = htmlHelper.ViewContext.RequestContext.HttpContext.Request.Url.AbsolutePath;
            pageSize = pageSize == 0 ? 3 : pageSize;
            var totalPages = Math.Max((totalCount + pageSize - 1) / pageSize, 1); //总页数
            var output = new StringBuilder();
            if (totalPages > 1)
            {
                //if (currentPage != 1)
                {//处理首页连接
                    output.AppendFormat("<a class='pageLink' href='{0}?pageIndex=1&pageSize={1}&id={2}'>首页</a> ", redirectTo, pageSize,id);
                }
                if (currentPage > 1)
                {//处理上一页的连接
                    output.AppendFormat("<a class='pageLink' href='{0}?pageIndex={1}&pageSize={2}&id={3}'>上一页</a> ", redirectTo, currentPage - 1, pageSize,id);
                }
                else
                {
                    // output.Append("<span class='pageLink'>上一页</span>");
                }

                output.Append(" ");
                int currint = 5;
                for (int i = 0; i <= 10; i++)
                {//一共最多显示10个页码，前面5个，后面5个
                    if ((currentPage + i - currint) >= 1 && (currentPage + i - currint) <= totalPages)
                    {
                        if (currint == i)
                        {//当前页处理
                            //output.Append(string.Format("[{0}]", currentPage));
                            output.AppendFormat("<a class='cpb' href='{0}?pageIndex={1}&pageSize={2}&id={4}'>{3}</a> ", redirectTo, currentPage, pageSize, currentPage,id);
                        }
                        else
                        {//一般页处理
                            output.AppendFormat("<a class='pageLink' href='{0}?pageIndex={1}&pageSize={2}&id={4}'>{3}</a> ", redirectTo, currentPage + i - currint, pageSize, currentPage + i - currint,id);
                        }
                    }
                    output.Append(" ");
                }
                if (currentPage < totalPages)
                {//处理下一页的链接
                    output.AppendFormat("<a class='pageLink' href='{0}?pageIndex={1}&pageSize={2}&id={3}'>下一页</a> ", redirectTo, currentPage + 1, pageSize,id);
                }
                else
                {
                    //output.Append("<span class='pageLink'>下一页</span>");
                }
                output.Append(" ");
                if (currentPage != totalPages)
                {
                    output.AppendFormat("<a class='pageLink' href='{0}?pageIndex={1}&pageSize={2}&id={3}'>末页</a> ", redirectTo, totalPages, pageSize,id);
                }
                output.Append(" ");
            }
            output.AppendFormat("第{0}页 / 共{1}页", currentPage, totalPages);//这个统计加不加都行

            return new HtmlString(output.ToString());
        }
    }
}