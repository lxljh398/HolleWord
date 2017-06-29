using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    class ScratchOff
    {
        static void Main(string[] args)
        {
            //int rnd = 0;
            //StreamWriter sw = new StreamWriter("E:\\ScratchOff.txt");
            //StringBuilder cwout = new StringBuilder();
            //for (int i = 0; i < 1000; i++)
            //{
            //    Console.WriteLine("第" + (i + 1) + "组：");
            //    cwout.Append("第" + (i + 1) + "组：" + "\r\n");
            //    rnd = GetRnd();
            //    //Console.WriteLine(rnd);
            //    cwout.Append((int)PercentageRandom(rnd) + "\r\n");
            //    //Console.WriteLine(PercentageRandom(rnd));
            //    cwout.Append(PercentageRandom(rnd) + "\r\n");
            //    //List<Card> list = GetLotteryNumber(PercentageRandom(rnd));
            //    //for (int j = 0; j < list.Count; j++)
            //    //{
            //    //    Console.WriteLine(list[j].Number + "--------" + list[j].Color);
            //    //    cwout.Append(list[j].Number + "--------" + list[j].Color + "\r\n");
            //    //}
            //}
            //sw.Write(cwout);
            //sw.Close();

            //string str = System.Environment.CurrentDirectory;
            //Console.WriteLine(str);


            //Console.WriteLine(NOTWINNING);
            //Console.WriteLine(PAIRS);
            //Console.WriteLine(TWOPAIRS);
            //Console.WriteLine(TRISBAR);
            //Console.WriteLine(STRAIGHT);
            //Console.WriteLine(SAMECOLOR);
            //Console.WriteLine(GOURD);
            //Console.WriteLine(BOMB);
            //Console.WriteLine(SAMECOLORSEQUENCE);

            //Console.WriteLine(NOTWINNING + PAIRS + TWOPAIRS + TRISBAR + STRAIGHT + SAMECOLOR + GOURD + BOMB + SAMECOLORSEQUENCE);
            //Console.ReadKey();



            PercentageRandom(2);


            //CreateCards();

        }

        private static List<Cards> CreateCards()
        {
            List<Cards> listCards = new List<Cards>();
            int rndNumber = GetRnd();
            LotteryWinCategories category = PercentageRandom(rndNumber);
            List<Card> list = GetLotteryNumber(category);
            Cards cards = new Cards()
            {
                RndNumber = rndNumber,
                WinCategory = category
            };
            listCards.Add(cards);
            for (int i = 0; i < 4; i++)
            {
                list = GetLotteryNumber(LotteryWinCategories.NotWinning);
                cards = new Cards()
                {
                    RndNumber = rndNumber,
                    WinCategory = category
                };
                listCards.Add(cards);
            }
            return listCards;
        }


        #region 定量   
        //倍率（不能设置为0）
        readonly static double PRECENTAGE = 1;
        //随机数的最大值
        readonly static int MAXRNDNUMBER = 100000;
        // 中奖概率值总和（56741）
        readonly static int WINCHANCEVALUE = Convert.ToInt32(56741 * PRECENTAGE);
        // 0出现的概率为%43.259 
        readonly static int NOTWINNING = WINCHANCEVALUE;//(中奖线到最大值为未中奖)
        // 20出现的概率为与中奖率比率 52.872% 
        readonly static int PAIRS = WINCHANCEVALUE- Convert.ToInt32(WINCHANCEVALUE * 0.52872);
        // 30出现的概率为与中奖率比率 26.436% 
        readonly static int TWOPAIRS = PAIRS - Convert.ToInt32(WINCHANCEVALUE * 0.26436);
        // 50出现的概率为与中奖率比率 17.624%
        readonly static int TRISBAR = TWOPAIRS - Convert.ToInt32(WINCHANCEVALUE * 0.17624);
        // 100出现的概率为与中奖率比率 1.762%
        readonly static int STRAIGHT = TRISBAR - Convert.ToInt32(WINCHANCEVALUE * 0.01762);
        // 200出现的概率为与中奖率比率 0.881%
        readonly static int SAMECOLOR = STRAIGHT - Convert.ToInt32(WINCHANCEVALUE * 0.00881);
        // 500出现的概率为与中奖率比率 0.352%
        readonly static int GOURD = SAMECOLOR - Convert.ToInt32(WINCHANCEVALUE * 0.00352);
        // 1000出现的概率为与中奖率比率 0.071%
        readonly static int BOMB = GOURD - Convert.ToInt32(WINCHANCEVALUE * 0.00071);
        // 10000出现的概率为与中奖率比率 0.001%
        readonly static int SAMECOLORSEQUENCE = BOMB - Convert.ToInt32(WINCHANCEVALUE * 0.00001);
        // 20000出现的概率为%0 
        readonly static double MAXIMUM = 0;
        readonly static String[] STRAIGHTCARDNUMBERS = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };//取顺子下标
        readonly static String[] CARDNUMBERS = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
        readonly static CardColors[] CARDCOLOR = new CardColors[] { CardColors.Hearts, CardColors.Spade, CardColors.Diamond, CardColors.Club };
        #endregion
        /// <summary>
        /// 通过随机数获取牌内容
        /// </summary>
        /// <param name="randomNumber"></param>
        /// <returns></returns>
        private static LotteryWinCategories PercentageRandom(int randomNumber)
        {
            if (randomNumber > NOTWINNING)
                return LotteryWinCategories.NotWinning;
            if (randomNumber > PAIRS && randomNumber <= NOTWINNING)
                return LotteryWinCategories.Pairs;
            if (randomNumber > TWOPAIRS && randomNumber <= PAIRS)
                return LotteryWinCategories.Twopairs;
            if (randomNumber > TRISBAR && randomNumber <= TWOPAIRS)
                return LotteryWinCategories.Trisbar;
            if (randomNumber > STRAIGHT && randomNumber <= TRISBAR)
                return LotteryWinCategories.Straight;
            if (randomNumber > SAMECOLOR && randomNumber <= STRAIGHT)
                return LotteryWinCategories.SameColor;
            if (randomNumber > GOURD && randomNumber <= SAMECOLOR)
                return LotteryWinCategories.Gourd;
            if (randomNumber > BOMB && randomNumber <= GOURD)
                return LotteryWinCategories.Bomb;
            if (randomNumber > SAMECOLORSEQUENCE && randomNumber <= BOMB)
                return LotteryWinCategories.SameColorSequence;
            if (randomNumber > MAXIMUM && randomNumber <= SAMECOLORSEQUENCE)
                return LotteryWinCategories.Maximum;
            return LotteryWinCategories.NotWinning;
        }
        #region 计算方法
        /// <summary>
        /// 通过中奖等级获取奖券内容
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        private static List<Card> GetLotteryNumber(LotteryWinCategories category)
        {
            List<Card> list = new List<Card>();
            switch (category)
            {
                case LotteryWinCategories.NotWinning:
                    list = GetLotteryNumber4NotWinning();
                    break;
                case LotteryWinCategories.Pairs:
                    list = GetLotteryNumber4Pair();
                    break;
                case LotteryWinCategories.Twopairs:
                    list = GetLotteryNumber4TwoPairs();
                    break;
                case LotteryWinCategories.Trisbar:
                    list = GetLotteryNumber4Trisbar();
                    break;
                case LotteryWinCategories.Straight:
                    list = GetLotteryNumber4Straight();
                    break;
                case LotteryWinCategories.SameColor:
                    list = GetLotteryNumber4SameColor();
                    break;
                case LotteryWinCategories.Gourd:
                    list = GetLotteryNumber4Gourd();
                    break;
                case LotteryWinCategories.Bomb:
                    list = GetLotteryNumber4Bomb();
                    break;
                case LotteryWinCategories.SameColorSequence:
                    list = GetLotteryNumber4SameColorSequence();
                    break;
                case LotteryWinCategories.Maximum:
                    list = GetLotteryNumber4Maximum();
                    break;
                default:
                    list = GetLotteryNumber4NotWinning();
                    break;
            }
            return list;
        }


        /// <summary>
        /// 不中奖内容
        /// </summary>
        /// <returns></returns>
        private static List<Card> GetLotteryNumber4NotWinning()
        {
            List<string> cardNumbers = GetRandomCardNumbers(5, CARDNUMBERS);
            List<CardColors> cardColors = GetRandomCardColor(5, CARDCOLOR);
            List<Card> list = new List<Card>();
            for (int i = 0; i < cardNumbers.Count; i++)
            {
                Card card = new Card()
                {
                    Number = cardNumbers[i],
                    Color = cardColors[i]
                };
                list.Add(card);
            }
            return list;
        }
        /// <summary>
        /// 20元/对子
        /// </summary>
        /// <returns></returns>
        private static List<Card> GetLotteryNumber4Pair()
        {
            List<string> cardNumbers = GetRandomCardNumbers(4, CARDNUMBERS);
            List<string> pairsNumber = GetRandomCardNumbers(1, cardNumbers.ToArray());
            List<CardColors> cardColors = GetRandomCardColor(4, CARDCOLOR);
            List<Card> list = GetPairs(cardNumbers, pairsNumber, cardColors);
            return list;
        }
        /// <summary>
        /// 30元/两对子
        /// </summary>
        /// <returns></returns>
        private static List<Card> GetLotteryNumber4TwoPairs()
        {
            List<string> cardNumbers = GetRandomCardNumbers(3, CARDNUMBERS);
            List<string> TwoPairsNumber = GetRandomCardNumbers(2, cardNumbers.ToArray());
            List<CardColors> cardColors = GetRandomCardColor(4, CARDCOLOR);
            List<Card> list = GetPairs(cardNumbers, TwoPairsNumber, cardColors);
            return list;
        }
        /// <summary>
        /// 50元/三条
        /// </summary>
        /// <returns></returns>
        private static List<Card> GetLotteryNumber4Trisbar()
        {
            List<string> cardNumbers = GetRandomCardNumbers(3, CARDNUMBERS);
            List<string> trisbarNumber = GetRandomCardNumbers(1, cardNumbers.ToArray());
            List<CardColors> cardColors = GetRandomCardColor(4, CARDCOLOR);
            List<Card> list = new List<Card>();
            Card card = new Card();
            for (int i = 0; i < cardNumbers.Count; i++)
            {
                card = new Card()
                {
                    Number = cardNumbers[i],
                    Color = cardColors[i]
                };
                list.Add(card);
            }
            string number = trisbarNumber[0];
            for (int i = 0; i < 2; i++)
            {
                CardColors[] noExistColors = list.Where(p => p.Number == number).Select(p => p.Color).ToArray();
                card = new Card()
                {
                    Number = number,
                    Color = GetRandomNoExistCardColor(1, noExistColors)[0]
                };
                list.Add(card);
            }
            return list;
        }
        /// <summary>
        /// 100元/顺子
        /// </summary>
        /// <returns></returns>
        private static List<Card> GetLotteryNumber4Straight()
        {
            int cardNumbersIndex = int.Parse(GetRandomCardNumbers(1, STRAIGHTCARDNUMBERS)[0]) - 1;//取随机下标
            List<Card> list = new List<Card>();
            Card card = new Card();
            for (int i = 0; i < 5; i++)
            {
                CardColors color = GetRandomCardColor(1, CARDCOLOR)[0];
                card = new Card()
                {
                    Number = CARDNUMBERS[cardNumbersIndex + i],
                    Color = color
                };
                list.Add(card);
            }
            return list;
        }
        /// <summary>
        /// 200元/同花
        /// </summary>
        /// <returns></returns>
        private static List<Card> GetLotteryNumber4SameColor()
        {
            List<string> cardNumbers = GetRandomCardNumbers(5, CARDNUMBERS);
            CardColors color = GetRandomCardColor(1, CARDCOLOR)[0];
            List<Card> list = new List<Card>();
            Card card = new Card();
            for (int i = 0; i < cardNumbers.Count; i++)
            {
                card = new Card()
                {
                    Number = cardNumbers[i],
                    Color = color
                };
                list.Add(card);
            }
            return list;
        }
        /// <summary>
        /// 500元/葫芦
        /// </summary>
        /// <returns></returns>
        private static List<Card> GetLotteryNumber4Gourd()
        {
            Random random = new Random();
            List<string> cardNumbers = GetRandomCardNumbers(2, CARDNUMBERS);
            List<Card> list = new List<Card>();
            list.AddRange(GetGourdOrBomb(cardNumbers[0], 2));
            list.AddRange(GetGourdOrBomb(cardNumbers[1], 3));
            return list;
        }
        /// <summary>
        /// 1000元/炸弹
        /// </summary>
        /// <returns></returns>
        private static List<Card> GetLotteryNumber4Bomb()
        {
            Random random = new Random();
            List<string> cardNumbers = GetRandomCardNumbers(2, CARDNUMBERS);
            List<Card> list = new List<Card>();
            list.AddRange(GetGourdOrBomb(cardNumbers[0], 1));
            list.AddRange(GetGourdOrBomb(cardNumbers[1], 4));
            return list;
        }
        /// <summary>
        /// 10000元/同花顺子
        /// </summary>
        /// <returns></returns>
        private static List<Card> GetLotteryNumber4SameColorSequence()
        {
            int cardNumbersIndex = int.Parse(GetRandomCardNumbers(1, STRAIGHTCARDNUMBERS)[0]) - 1;//取随机下标
            CardColors color = GetRandomCardColor(1, CARDCOLOR)[0];
            List<Card> list = new List<Card>();
            Card card = new Card();
            for (int i = 0; i < 5; i++)
            {
                card = new Card()
                {
                    Number = CARDNUMBERS[cardNumbersIndex + i],
                    Color = color
                };
                list.Add(card);
            }
            return list;
        }
        /// <summary>
        /// 20000元/皇家同花顺
        /// </summary>
        /// <returns></returns>
        private static List<Card> GetLotteryNumber4Maximum()
        {
            int cardNumbersIndex = 8;//取随机下标
            CardColors color = GetRandomCardColor(1, CARDCOLOR)[0];
            List<Card> list = new List<Card>();
            Card card = new Card();
            for (int i = 0; i < 5; i++)
            {
                card = new Card()
                {
                    Number = CARDNUMBERS[cardNumbersIndex + i],
                    Color = color
                };
                list.Add(card);
            }
            return list;
        }
        #endregion

        #region CommonMathods        
        private static List<Card> GetGourdOrBomb(string number, int count)
        {
            List<Card> newlist = new List<Card>();
            for (int i = 0; i < count; i++)
            {
                CardColors[] noExistColors = newlist.Where(p => p.Number == number).Select(p => p.Color).ToArray();
                if (noExistColors == null)
                    noExistColors = GetRandomCardColor(1, CARDCOLOR).ToArray();
                Card card = new Card()
                {
                    Number = number,
                    Color = GetRandomNoExistCardColor(1, noExistColors)[0]
                };
                newlist.Add(card);
            }
            return newlist;
        }
        private static List<Card> GetPairs(List<string> cardNumbers, List<string> pairsNumber, List<CardColors> cardColors)
        {
            List<Card> list = new List<Card>();
            Card card = new Card();
            for (int i = 0; i < cardNumbers.Count; i++)
            {
                card = new Card()
                {
                    Number = cardNumbers[i],
                    Color = cardColors[i]
                };
                list.Add(card);
            }
            for (int i = 0; i < pairsNumber.Count; i++)
            {
                string number = pairsNumber[i];
                CardColors[] noExistColors = list.Where(p => p.Number == number).Select(p => p.Color).ToArray();
                card = new Card()
                {
                    Number = number,
                    Color = GetRandomNoExistCardColor(1, noExistColors)[0]
                };
                list.Add(card);
            }
            return list;
        }
        /// <summary>
        /// 卡片
        /// </summary>
        private class Card
        {
            public string Number { get; set; }
            public CardColors Color { get; set; }
        }
        /// <summary>
        /// 卡片列表
        /// </summary>
        private class Cards
        {
            public int RndNumber { get; set; }
            public LotteryWinCategories WinCategory { get; set; }
            public string CardDetail { get; set; }
        }
        /// <summary>
        /// 取随机N张不重复的牌数字
        /// </summary>
        /// <param name="getNumber"></param>
        /// <returns></returns>
        private static List<string> GetRandomCardNumbers(int getNumber, string[] nums)
        {
            Random random = new Random();
            int a = random.Next(13);
            List<string> numbers = nums.OrderBy(x => random.Next()).Take(getNumber).ToList();
            return numbers;
        }
        /// <summary>
        /// 取不重复随机的几个花色
        /// </summary>
        /// <param name="getNumber"></param>
        /// <returns></returns>
        private static List<CardColors> GetRandomNoRepeatCardColor(int getNumber, CardColors[] colors)
        {
            Random random = new Random();
            int a = random.Next(0, 4) + 1;
            List<CardColors> list = colors.OrderBy(x => random.Next()).Take(getNumber).ToList();
            return list;
        }
        /// <summary>
        /// 排除某个色取随机花色
        /// </summary>
        /// <param name="getNumber"></param>
        /// <returns></returns>
        private static List<CardColors> GetRandomNoExistCardColor(int getNumber, CardColors[] noExistColor)
        {
            CardColors[] colors = CARDCOLOR.Except(noExistColor).ToArray();
            Random random = new Random();
            int a = random.Next(0, 4) + 1;
            List<CardColors> list = colors.OrderBy(x => random.Next()).Take(getNumber).ToList();
            return list;
        }
        /// <summary>
        /// 取随机的几个花色
        /// </summary>
        /// <param name="getNumber"></param>
        /// <returns></returns>
        private static List<CardColors> GetRandomCardColor(int getNumber, CardColors[] colors)
        {
            Random random = new Random();
            List<CardColors> list = new List<CardColors>();
            for (int i = 0; i < getNumber; i++)
            {
                int color = random.Next(0, 4) + 1;
                list.Add((CardColors)color);
            }
            return list;
        }
        /// <summary>
        /// 产生随机数
        /// </summary>
        /// <returns></returns>
        private static int GetRnd()
        {
            //这样产生固定的随机数（注释举例： 0 ~ 100000的强随机数（含100000））
            int max = MAXRNDNUMBER;
            int rnd = int.MinValue;
            decimal _base = (decimal)long.MaxValue;
            byte[] rndSeries = new byte[8];
            //RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            //rng.Dispose();
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(rndSeries);
                //不含100000需去掉+1
                rnd = (int)(Math.Abs(BitConverter.ToInt64(rndSeries, 0)) / _base * (max + 1));
                return rnd;
            }
        }
        #endregion


        /// <summary>
        /// 牌花色
        /// </summary>
        public enum CardColors
        {
            /// <summary>
            /// 红桃
            /// </summary>
            Hearts = 1,
            /// <summary>
            /// 黑桃
            /// </summary>
            Spade = 2,
            /// <summary>
            /// 方块
            /// </summary>
            Diamond = 3,
            /// <summary>
            /// 梅花
            /// </summary>
            Club = 4
        }


        /// <summary>
        /// 开奖类别
        /// </summary>
        public enum LotteryWinCategories
        {
            /// <summary>
            /// 未中奖
            /// </summary>
            NotWinning = 0,
            /// <summary>
            /// 对子
            /// </summary>
            Pairs = 20,
            /// <summary>
            /// 两对
            /// </summary>
            Twopairs = 30,
            /// <summary>
            /// 三条
            /// </summary>
            Trisbar = 50,
            /// <summary>
            /// 顺子
            /// </summary>
            Straight = 100,
            /// <summary>
            /// 同花
            /// </summary>
            SameColor = 200,
            /// <summary>
            /// 葫芦
            /// </summary>
            Gourd = 500,
            /// <summary>
            /// 炸弹
            /// </summary>
            Bomb = 1000,
            /// <summary>
            /// 同花顺
            /// </summary>
            SameColorSequence = 10000,
            /// <summary>
            /// 皇家同花顺
            /// </summary>
            Maximum = 200000
        }
    }
}
