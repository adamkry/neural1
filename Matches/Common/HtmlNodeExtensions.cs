using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Matches.Common
{
    public static class HtmlNodeExtensions
    {
        private static HtmlNode GetRelatedNodeWhere(HtmlNode node, Func<HtmlNode, HtmlNode> getRelated, Func<HtmlNode, bool> where)
        {
            HtmlNode currentNode = node;
            HtmlNode result = null;
            while (result == null && currentNode != null)
            {
                currentNode =  getRelated(currentNode);
                if (where(currentNode))
                {
                    result = currentNode;
                }
            }
            return result;
        }

        public static HtmlNode GetNextSiblingWhere(this HtmlNode node, Func<HtmlNode, bool> where)
        {
            return GetRelatedNodeWhere(node, n => n.NextSibling, where);
        }

        public static HtmlNode GetPreviousSiblingWhere(this HtmlNode node, Func<HtmlNode, bool> where)
        {
            return GetRelatedNodeWhere(node, n => n.PreviousSibling, where);
        }

        public static HtmlNode GetParentWhere(this HtmlNode node, Func<HtmlNode, bool> where)
        {
            return GetRelatedNodeWhere(node, n => n.ParentNode, where);
        }
    }
}
