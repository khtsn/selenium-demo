using System.Collections.ObjectModel;

using OpenQA.Selenium;

namespace SeleniumBasic.Utilities
{
    public static class TableUtils
    {
        public static IWebElement[,] getTableData(ReadOnlyCollection<IWebElement> headerData, ReadOnlyCollection<IWebElement> contentData)
        {
            int totalRowData = (contentData.Count / headerData.Count);
            IWebElement[,] table = new IWebElement[totalRowData + 1, headerData.Count]; //+1 for header

            for (int i = 0; i < headerData.Count; i++)
            {
                table[0, i] = headerData[i];
            }

            for (int i = 1; i <= totalRowData; i++)
            {
                for (int j = 0; j < headerData.Count; j++)
                {
                    table[i, j] = contentData[headerData.Count * (i - 1) + j];
                }
            }

            return table;
        }
    }
}
