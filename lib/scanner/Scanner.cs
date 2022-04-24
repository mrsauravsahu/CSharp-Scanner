using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace mrsauravsahu.scanner
{
    public class Scanner
    {
        private List<object> values = new List<object>();
        private string delimiter;
        private TextReader inputStream;

        public Scanner(string delimiter = " ")
        {
            this.delimiter = delimiter;
            this.inputStream = Console.In;
        }
        public Scanner(TextReader inputStream, string delimiter = " ")
        {
            this.delimiter = delimiter;
            this.inputStream = inputStream;
        }

        private void readIfNecessary()
        {
            if (values.Count == 0)
            {
                var line = inputStream.ReadLine();
                if (line is not null)
                {
                    values.AddRange((Regex.Split(line, delimiter)));
                    values.RemoveAll(p => string.Compare(p.ToString(), "") == 0);
                }
            }
        }
        public bool HasNext()
        {
            return (values.Count > 0);
        }
        public bool HasNext<T>() where T : IConvertible
        {
            T value;
            if (values.Count == 0) throw new EndOfStreamException("Input Buffer was empty. No input was provided");
            value = (T)Convert.ChangeType(values[0], typeof(T));
            return true;
        }
        public T ReadNext<T>() where T : IConvertible
        {
            T value;
            ReadNext<T>(out value);
            return value;
        }
        public void ReadNext<T>(out T value) where T : IConvertible
        {
            readIfNecessary();
            if (values.Count == 0) throw new EndOfStreamException("Input Buffer was empty. No input was provided");
            value = (T)Convert.ChangeType(values[0], typeof(T));
            values.RemoveAt(0);
        }
        public List<T> ReadAllNext<T>() where T : IConvertible
        {
            List<T> all;
            ReadAllNext<T>(out all);
            return all;
        }
        public void ReadAllNext<T>(out List<T> values) where T : IConvertible
        {
            readIfNecessary();
            values = new List<T>();
            while (HasNext<T>())
            {
                if (this.values.Count == 0) throw new EndOfStreamException("Input Buffer was empty. No input was provided");
                values.Add((T)Convert.ChangeType(this.values[0], typeof(T)));
                this.values.RemoveAt(0);
            }
        }
    }
}
