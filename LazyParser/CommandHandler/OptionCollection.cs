using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LazyParser.CommandHandler
{
    public class OptionCollection : IEnumerable<OptionData>
    {
        private readonly List<OptionData> Options = new List<OptionData>();

        public IEnumerator<OptionData> GetEnumerator()
        {
            return Options.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Options.GetEnumerator();
        }

        public void Add(OptionData option)
        {
            if (Options.Count(x => x.Name == option.Name) > 0)
            {
                throw new Exception("Option already have.");
            }
            Options.Add(option);
        }

        public bool Contains(string name) 
        {
            if (this[name] == null)
            {
                return false;
            }
            return true;
        }

        public void AddRange(IEnumerable<OptionData> options)
        {
            foreach (var item in options)
            {
                Add(item);
            }
        }

        public OptionData this[string name] 
        {
            get 
            {
                return Options.Where(x => x.Name == name).FirstOrDefault();
            }
        }
    }
}
