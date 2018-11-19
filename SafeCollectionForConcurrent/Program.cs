using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeCollectionForConcurrent
{
    class Program
    {
        //1、将使用数组和不安全集合的代码转换为使用并发集合的代码
        //   可以使用任何支持IEnumerable 接口的集合 集合中的元素的数据类型=并发集合为其元素所定义的数据类型一致
        //问题：
        //   线程安全带来的额外开销可能导致代码运行速度要比串行代码慢
        //解决：
        //   并发队列中的元素创建一个不安全的集合
        //   CopyTo 
        //   ToArray
        private static string[] _invalidHexValues = { "af", "bd", "bf", "cf", "da", "fa", "fe", "ff" };


        static void Main(string[] args)
        {
            #region 参考
            ////提供了有参构造和无参构造
            //var invalidHexValueStack = new ConcurrentStack<string>(_invalidHexValues);

            ////转换
            ////string[] invalidStrings = new string[invalidHexValueStack.Count];
            ////invalidHexValueStack.CopyTo(invalidStrings, 0);
            ////invalidStrings = invalidHexValueStack.ToArray();

            //while (!invalidHexValueStack.IsEmpty)
            //{
            //    string hexValue;
            //    invalidHexValueStack.TryPop(out hexValue);
            //    Console.WriteLine(hexValue);
            //}
            #endregion

            #region 
            var sw = Stopwatch.StartNew();

            _sentencesBag = new ConcurrentBag<string>();
            _capWordsInsentenceBag = new ConcurrentBag<string>();
            _finalSentenceBag = new ConcurrentBag<string>();

            _productingSentences = true;

            Parallel.Invoke(
                () => ProducesSentences(), //生产者
                () => CapitalizeWordsInSentences(),//消费者-生产者
                () => RemoveLetterInSentences()//消费者
                );
            Console.WriteLine("Number Of Sentence with capitalized words in the bag:{0}", _capWordsInsentenceBag.Count);
            Console.WriteLine("Number Of Sentence with removed letters in the bag:{0}", _finalSentenceBag.Count);
            Debug.WriteLine(sw.Elapsed.ToString());
            Console.WriteLine();
            #endregion

            Console.ReadLine();
        }

        /// <summary>
        /// 多个生产者和消费者从多个ConcurrentBag实例添加或删除元素
        /// </summary>
        static string RemoveLetters(char[] letters, string sentence)
        {
            var sb = new StringBuilder();
            bool match = false;
            for (int i = 0; i < sentence.Length; i++)
            {
                for (int j = 0; j < letters.Length; j++)
                {
                    if (sentence[i] == letters[j])
                    {
                        match = true;
                        break;
                    }
                }
                if (!match)
                {
                    sb.Append(sentence[i]);
                }
            }
            return sb.ToString();
        }

        static string CapitallizeWords(char[] delimeters, string sentence, char newDelimeter)
        {
            string[] words = sentence.Split(delimeters);
            var sb = new StringBuilder();
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Length > 1)
                {
                    //首字母大写
                    sb.Append(words[i][0].ToString().ToUpper());
                    sb.Append(words[i].Substring(1).ToLower());
                }
                else
                {
                    sb.Append(words[i].ToLower());
                }
            }
            return sb.ToString();
        }

        const int NUM_SENTENCES = 2000000;
        static ConcurrentBag<string> _sentencesBag;
        static ConcurrentBag<string> _capWordsInsentenceBag;
        static ConcurrentBag<string> _finalSentenceBag;
        static volatile bool _productingSentences = false;
        static volatile bool _capitalizingWords = false;

        static void ProducesSentences()
        {
            string[] possibleSentence = {
                "ConcurrentBag is included in the System.Concurrent.Collections namespace.",
                "Is parallelism important for cloud-computing?",
                "Parallelism is very important for cloud-computing!",
                "ConcurrentQueue is one of the new concurrent collections added in .NET Framework 4",
                "ConcurrentStack is a concurrent collection that represents a LIFO collection.",
                "ConcurrentQueue is a concurrent collection that represents a FIFO collection."
            };
            try
            {
                var rnd = new Random();
                for (int i = 0; i < NUM_SENTENCES; i++)
                {
                    var sb = new StringBuilder();
                    for (int j = 0; j < possibleSentence.Length; j++)
                    {
                        if (rnd.Next(2) > 0)
                        {
                            sb.Append(possibleSentence[rnd.Next(possibleSentence.Length)]);
                            sb.Append(' ');
                        }
                    }
                    //返回小于20的随机整数 3/4
                    if (rnd.Next(20) > 15)
                    {
                        _sentencesBag.Add(sb.ToString());
                    }
                    else
                    {
                        _sentencesBag.Add(sb.ToString().ToUpper());
                    }
                }
            }
            finally
            {
                _productingSentences = false;
            }
        }

        static void CapitalizeWordsInSentences()
        {
            char[] delimiterChars = { ' ', ',', '.', ':', ';', '(', ')', '{', '}',
                '[', ']', '/', '?', '@', '\t', '?', '"' };
            //Start after Produce sentences began working
            System.Threading.SpinWait.SpinUntil(() => _productingSentences);
            try
            {
                _capitalizingWords = true;
                while ((!_sentencesBag.IsEmpty) || (_productingSentences))
                {
                    string sentences;
                    if (_sentencesBag.TryTake(out sentences))
                    {
                        _capWordsInsentenceBag.Add(CapitallizeWords(delimiterChars, sentences, '\\'));
                    }
                }
            }
            finally
            {
                _capitalizingWords = false;
            }
        }
        static void RemoveLetterInSentences()
        {
            char[] letterChars =
            {
                'A','B','C','e','i','j','m','X','y','Z'
            };
            System.Threading.SpinWait.SpinUntil(() => _capitalizingWords);
            while ((!_capWordsInsentenceBag.IsEmpty) || (_capitalizingWords))
            {
                string sentence;
                if (_capWordsInsentenceBag.TryTake(out sentence))
                {
                    _finalSentenceBag.Add(RemoveLetters(letterChars, sentence));
                }
            }

        }
    }
}
