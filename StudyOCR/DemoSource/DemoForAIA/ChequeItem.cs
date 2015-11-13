using System.Collections.Generic;
using System.Drawing;

namespace DemoForAIA
{
    public class ChequeItem
    {
        public string TxNo { get; set; }
        public string BatchNo { get; set; }
        public double TotalAmt { get; set; }
        public string ChequeNo { get; set; }
        public Dictionary<int, List<string>> dicDetail { get; set; }

        public Rectangle RcTxNo { get; private set; }
        public Rectangle RcBatchNo { get; private set; }
        public Rectangle RcTotalAmt { get; private set; }
        public Rectangle RcListDetail { get; private set; }

        public Rectangle RcSignature { get; private set; }

#if DEBUG
        public Rectangle RcChequeNo { get; private set; }
#endif

        public ChequeItem(string pTxNo, string pChequeNo, string pBatchNo, double pTotalAmt, List<string> pListDetail)
        {
            this.TxNo = pTxNo;
            this.ChequeNo = pChequeNo;
            this.BatchNo = pBatchNo;
            this.TotalAmt = pTotalAmt;

            this.dicDetail = new Dictionary<int, List<string>>();
            int pageIndex = 1;
            int minCount = 10;
            int maxCount = 25;
            int rangeIndex = minCount;
            int totalRecs = pListDetail.Count;
            int remainRecs = totalRecs - rangeIndex;
            while (remainRecs > 0)
            {
                if (pageIndex == 1)
                {
                    this.dicDetail.Add(pageIndex, pListDetail.GetRange(0, minCount));
                }
                else
                {
                    this.dicDetail.Add(pageIndex, pListDetail.GetRange((rangeIndex * (pageIndex - 2)) + minCount, maxCount));
                }

                pageIndex++;
                rangeIndex = maxCount;
                remainRecs = totalRecs - ((rangeIndex * (pageIndex - 1)) + minCount);
            }

            if (pageIndex == 1)
                this.dicDetail.Add(pageIndex, pListDetail.GetRange(0, remainRecs + rangeIndex));
            else
                this.dicDetail.Add(pageIndex, pListDetail.GetRange((rangeIndex * (pageIndex - 2)) + minCount, remainRecs + rangeIndex));

            this.RcTxNo = new Rectangle(100, 100, 310, 30);
            this.RcBatchNo = new Rectangle(100, 150, 627, 30);
            this.RcTotalAmt = new Rectangle(100, 200, 627, 30);
            this.RcListDetail = new Rectangle(100, 250, 627, 30);
            this.RcSignature = new Rectangle(100, 800, 627, 100);

#if DEBUG
            this.RcChequeNo = new Rectangle(411, 100, 310, 30);
#endif
        }
    }
}
