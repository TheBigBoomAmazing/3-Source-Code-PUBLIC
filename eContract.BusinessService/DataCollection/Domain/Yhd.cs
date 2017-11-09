using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TopDATA.BusinessService.DataCollection.Common;
using Winista.Text.HtmlParser;
using Winista.Text.HtmlParser.Filters;
using Winista.Text.HtmlParser.Lex;
using Winista.Text.HtmlParser.Util;

namespace TopDATA.BusinessService.DataCollection.Domain
{
    public class Yhd : IDataCollection
    {
        private readonly DataTable _dtGoods;
        private readonly DataTable _dtTopEnterprise;

        public Yhd(string enterpriseCode)
        {
            if (_dtGoods == null)
                _dtGoods = DataHelper.GetGoodsByEnterpriseCode(enterpriseCode);
            if (_dtTopEnterprise == null)
                _dtTopEnterprise = DataHelper.GetTopEnterpriseByEnterpriseCode(enterpriseCode);
        }

        public DataTable InventoryData(string strBuff)
        {
            #region 分析网页html节点

            DataTable dt = new DataTable();
            dt.Columns.Add("ArticleNo");//商品编码
            dt.Columns.Add("StoreCode");//区域/仓库
            dt.Columns.Add("StoreName");//区域/仓库
            dt.Columns.Add("Inventory");//总库存
            dt.Columns.Add("Date");
            dt.Columns.Add("BarCode");
            dt.Columns.Add("TopStoreCode");
            dt.Columns.Add("TopStoreName");


            for (int j = 1; j <= 1; j++)//分页
            {
                Lexer lexer = new Lexer(strBuff);
                Parser parser = new Parser(lexer);
                NodeFilter html = new TagNameFilter("Table");
                NodeList htmlNodes = parser.Parse(html);

                string strArticleNo = "";
                int rowspan = 1;
                int count = 1;
                for (int i = 9; i <= htmlNodes[1].Children.Count - 4; i++)//空格也算一个元素
                {
                    if (count == rowspan) rowspan = 1;
                    string strStoreCode;
                    string strStoreName;
                    if (rowspan > 1)//多列并排 取上一列的值
                    {
                        strStoreCode = htmlNodes[1].Children[i].Children[1].ToPlainTextString();
                        strStoreName = htmlNodes[1].Children[i].Children[1].ToPlainTextString();
                        if (count <= rowspan) count++;
                    }
                    else
                    {
                        //HTML解析商品编码
                        var htmlArticleNo = htmlNodes[1].Children[i].Children[1].ToHtml().Replace("  ", " ").Replace("\"", "");
                        rowspan = GetStrArticleNo(htmlArticleNo);
                        strArticleNo = htmlNodes[1].Children[i].Children[1].ToPlainTextString();
                        strStoreCode = htmlNodes[1].Children[i].Children[9].ToPlainTextString();
                        strStoreName = htmlNodes[1].Children[i].Children[9].ToPlainTextString();
                        count = 1;
                    }

                    string strInventory = htmlNodes[1].Children[i].Children[htmlNodes[1].Children[i].Children.Count - 2].ToPlainTextString();
                    string strDate = DateTime.Now.ToString("yyyy-MM-dd");

                    //获取条码
                    string barCode = DataHelper.GetBarCode(strArticleNo, _dtGoods);

                    //获取客户编号及客户简称
                    string strTopStoreCode = "";
                    string strTopStoreName = "";
                    DataHelper.GetTopStrore(strStoreCode, _dtTopEnterprise, ref strTopStoreCode, ref strTopStoreName);

                    DataRow dr = dt.NewRow();
                    dr[0] = strArticleNo;
                    dr[1] = strStoreCode;
                    dr[2] = strStoreName;
                    dr[3] = strInventory;
                    dr[4] = strDate;
                    dr[5] = barCode;
                    dr[6] = strTopStoreCode;
                    dr[7] = strTopStoreName;

                    dt.Rows.Add(dr);
                    i++;
                }
            }

            #endregion

            return dt;
        }

        public DataTable SalesData(string strBuff)
        {
            #region 分析网页html节点

            DataTable dt = new DataTable();
            dt.Columns.Add("ArticleNo");//商品编码
            dt.Columns.Add("BarCode");//商品条码
            dt.Columns.Add("StoreCode");//区域/仓库
            dt.Columns.Add("StoreName");//区域/仓库
            dt.Columns.Add("Sales");//销售
            dt.Columns.Add("Date");
            dt.Columns.Add("TopStoreCode");
            dt.Columns.Add("TopStoreName");
            for (int j = 1; j <= 1; j++)//分页
            {
                Lexer lexer = new Lexer(strBuff);
                Parser parser = new Parser(lexer);
                NodeFilter html = new TagNameFilter("Table");
                NodeList htmlNodes = parser.Parse(html);

                for (int i = 9; i <= htmlNodes[1].Children.Count - 4; i++)//空格也算一个元素
                {
                    string strArticleNo = htmlNodes[1].Children[i].Children[1].ToPlainTextString();
                    string salesBarCode = htmlNodes[1].Children[i].Children[5].ToPlainTextString();
                    string strStoreCode = htmlNodes[1].Children[i].Children[11].ToPlainTextString();
                    string strStoreName = htmlNodes[1].Children[i].Children[11].ToPlainTextString();
                    string strSales = "";
                    if (htmlNodes[1].Children[i].Children != null)
                    {
                        int sold = int.Parse(htmlNodes[1].Children[i].Children[13].ToPlainTextString());
                        int back = int.Parse(htmlNodes[1].Children[i].Children[15].ToPlainTextString());
                        strSales = (sold - back).ToString();
                    }
                    string strDate = DateTime.Now.ToString("yyyy-MM-dd");

                    //获取货号
                    if (string.IsNullOrEmpty(strArticleNo))
                        strArticleNo = DataHelper.GetArticalNo(salesBarCode, _dtGoods);

                    //获取客户编号及客户简称
                    string strTopStoreCode = "";
                    string strTopStoreName = "";
                    DataHelper.GetTopStrore(strStoreCode, _dtTopEnterprise, ref strTopStoreCode, ref strTopStoreName);

                    DataRow dr = dt.NewRow();
                    dr[0] = strArticleNo;
                    dr[1] = salesBarCode;
                    dr[2] = strStoreCode;
                    dr[3] = strStoreName;
                    dr[4] = strSales;
                    dr[5] = strDate;
                    dr[6] = strTopStoreCode;
                    dr[7] = strTopStoreName;

                    dt.Rows.Add(dr);
                    i++;
                }
            }

            #endregion

            return dt;
        }

        public DataTable OrderData(string strBuff)
        {
            throw new NotImplementedException();
        }

        private static int GetStrArticleNo(string htmlArticleNo)
        {
            var regexStr = "<td class=vt rowspan=(?<key>.*?)>";
            Regex reg = new Regex(regexStr, RegexOptions.None);
            Match mc = reg.Match(htmlArticleNo);
            int rowspan;
            int.TryParse(mc.Groups["key"].Value, out rowspan);
            return rowspan;
        }

    }
}
