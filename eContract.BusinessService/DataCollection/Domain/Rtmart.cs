using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using TopDATA.BusinessService.DataCollection.Common;
using Winista.Text.HtmlParser;
using Winista.Text.HtmlParser.Filters;
using Winista.Text.HtmlParser.Lex;
using Winista.Text.HtmlParser.Util;

namespace TopDATA.BusinessService.DataCollection.Domain
{
    public class Rtmart:IDataCollection
    {

        private readonly DataTable _dtGoods;
        private readonly DataTable _dtTopEnterprise;

        public Rtmart(string customerCode)
        {
            if (_dtGoods == null)
                _dtGoods = DataHelper.GetGoodsByCustomerCode(customerCode);
            if (_dtTopEnterprise == null)
                _dtTopEnterprise = DataHelper.GetTopEnterpriseByCustomerCode(customerCode);
        }
      
        public DataTable InventoryData(string strBuff)
        {
            #region 分析网页html节点

            DataTable dt = new DataTable();
            dt.Columns.Add("ArticleNo");
            dt.Columns.Add("StoreCode");
            dt.Columns.Add("StoreName");
            dt.Columns.Add("Inventory");
            dt.Columns.Add("Sales");
            dt.Columns.Add("Date");
            dt.Columns.Add("BarCode");
            dt.Columns.Add("TopStoreCode");
            dt.Columns.Add("TopStoreName");

            Lexer lexer = new Lexer(strBuff);
            Parser parser = new Parser(lexer);
            NodeFilter html = new TagNameFilter("Table");

            NodeList htmlNodes = parser.Parse(html);

            for (int j = 1; j <= 1; j++)
            {
                lexer = new Lexer(strBuff);
                parser = new Parser(lexer);
                html = new TagNameFilter("Table");

                htmlNodes = parser.Parse(html);

                Lexer lexers = new Lexer(htmlNodes[22].ToHtml());
                Parser parsers = new Parser(lexers);

                NodeFilter htmls = new TagNameFilter("TR");
                NodeList htmlNode = parsers.Parse(htmls);

                string strArticleNo = string.Empty;
                for (int i = 2; i < htmlNode.Count; i++)
                {
                    if (htmlNode[i].Children[0].Children != null)
                    {
                        strArticleNo = htmlNode[i].Children[0].Children[0].ToHtml().Replace("&nbsp;", "");
                    }
                    string strStoreCode = htmlNode[i].Children[2].Children[0].ToHtml().Split('-')[0];
                    string strStoreName = htmlNode[i].Children[2].Children[0].ToHtml().Split('-')[1];
                    string strInventory = htmlNode[i].Children[4].Children[0].ToHtml();
                    string strSales = htmlNode[i].Children[9].Children[0].ToHtml();
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
                    dr[4] = strSales;
                    dr[5] = strDate;
                    dr[6] = barCode;
                    dr[7] = strTopStoreCode;
                    dr[8] = strTopStoreName;
                    dt.Rows.Add(dr);
                }
            }

            #endregion

            return dt;
        }

        public DataTable SalesData(string strBuff)
        {
            #region 分析网页html节点

            DataTable dt = new DataTable();
            dt.Columns.Add("ArticleNo");
            dt.Columns.Add("StoreCode");
            dt.Columns.Add("StoreName");
            dt.Columns.Add("Inventory");
            dt.Columns.Add("Sales");
            dt.Columns.Add("Date");
            dt.Columns.Add("BarCode");
            dt.Columns.Add("TopStoreCode");
            dt.Columns.Add("TopStoreName");

            Lexer lexer = new Lexer(strBuff);
            Parser parser = new Parser(lexer);
            NodeFilter html = new TagNameFilter("Table");

            NodeList htmlNodes = parser.Parse(html);

            for (int j = 1; j <= 1; j++)
            {
                lexer = new Lexer(strBuff);
                parser = new Parser(lexer);
                html = new TagNameFilter("Table");

                htmlNodes = parser.Parse(html);

                Lexer lexers = new Lexer(htmlNodes[22].ToHtml());
                Parser parsers = new Parser(lexers);

                NodeFilter htmls = new TagNameFilter("TR");
                NodeList htmlNode = parsers.Parse(htmls);

                string strArticleNo = string.Empty;
                for (int i = 2; i < htmlNode.Count; i++)
                {
                    if (htmlNode[i].Children[0].Children != null)
                    {
                        strArticleNo = htmlNode[i].Children[0].Children[0].ToHtml().Replace("&nbsp;", "");
                    }
                    string strStoreCode = htmlNode[i].Children[2].Children[0].ToHtml().Split('-')[0];
                    string strStoreName = htmlNode[i].Children[2].Children[0].ToHtml().Split('-')[1];
                    string strInventory = htmlNode[i].Children[4].Children[0].ToHtml();
                    string strSales = htmlNode[i].Children[9].Children[0].ToHtml();
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
                    dr[4] = strSales;
                    dr[5] = strDate;
                    dr[6] = barCode;
                    dr[7] = strTopStoreCode;
                    dr[8] = strTopStoreName;
                    dt.Rows.Add(dr);
                }
            }

            #endregion

            return dt;
        }

        public DataTable OrderData(string strBuff)
        {
            #region 分析网页html节点

            DataTable dt = new DataTable();
            dt.Columns.Add("ArticleNo");
            dt.Columns.Add("StoreCode");
            dt.Columns.Add("StoreName");
            dt.Columns.Add("OrderQuantity");
            dt.Columns.Add("OrderAmount");
            dt.Columns.Add("OrderNo");
            dt.Columns.Add("BarCode");
            dt.Columns.Add("TopStoreCode");
            dt.Columns.Add("TopStoreName");
            Lexer lexerOrder = new Lexer(strBuff);
            Parser parserOrder = new Parser(lexerOrder);
            NodeFilter htmlOrder = new TagNameFilter("Table");

            NodeList htmlNodeOrders = parserOrder.Parse(htmlOrder);
            Lexer lexerOrders = new Lexer(htmlNodeOrders[21].ToHtml());
            Parser parserOrders = new Parser(lexerOrders);

            NodeFilter htmlOrderss = new TagNameFilter("A");
            NodeList htmlNodeOrder = parserOrders.Parse(htmlOrderss);
            for (int i = 0; i < htmlNodeOrder.Count; i++)
            {

                string strOrderNumber = htmlNodeOrder[i].Children[0].ToHtml();
                if (htmlNodeOrder[i].ToHtml().Contains("*"))
                    strOrderNumber = htmlNodeOrder[i].Children[1].ToHtml();
                string strStoreCode = string.Empty;
                if (htmlNodeOrder[i] is ITag)
                {

                    ITag tag = (htmlNodeOrder[i] as ITag);

                    if (!tag.IsEndTag())
                    {
                        if (tag.Attributes != null && tag.Attributes.Count > 0)
                        {
                            if (tag.Attributes["HREF"] != null)
                            {
                                strStoreCode = tag.Attributes["HREF"].ToString();
                                strStoreCode = strStoreCode.Split('=')[2];
                            }
                        }
                    }

                }

                if (!string.IsNullOrEmpty(strStoreCode))
                {
                    string path2 = AppDomain.CurrentDomain.BaseDirectory + @"Areas\\BusinessData\Test" + $@"\b_{strOrderNumber.Trim('*')}_{strStoreCode}.txt"; ;
                    string strBuff2 = DataHelper.Read(path2, Encoding.Default);
                    if (string.IsNullOrEmpty(strBuff2)) continue;
                    Lexer lexer = new Lexer(strBuff2);
                    Parser parser = new Parser(lexer);
                    NodeFilter html = new TagNameFilter("Table");

                    NodeList htmlNodes = parser.Parse(html);
                    Lexer lexers = new Lexer(htmlNodes[23].ToHtml());
                    Parser parsers = new Parser(lexers);

                    NodeFilter htmls = new TagNameFilter("TR");
                    NodeList htmlNode = parsers.Parse(htmls);

                    string strArticleNo = string.Empty;
                    for (int j = 2; j < htmlNode.Count; j++)
                    {

                        strArticleNo = htmlNode[j].Children[1].Children[0].ToHtml();
                        string strStoreName = string.Empty;

                        string strOrderQuantity = string.Empty;
                        if (strOrderNumber.Contains("*"))
                            strOrderQuantity = htmlNode[j].Children[6].Children[0].ToHtml();
                        else
                        {
                            strOrderQuantity = htmlNode[j].Children[5].Children[0].ToHtml();
                        }
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
                        dr[3] = strOrderQuantity;
                        dr[4] = "0";
                        dr[5] = strOrderNumber.Trim('*');
                        dt.Rows.Add(dr);
                    }
                }
            }
            #endregion

            return dt;
        }
    }
}
