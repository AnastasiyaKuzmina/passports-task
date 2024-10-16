using System.Data;
using System.Text.RegularExpressions;

namespace PassportApplication.Services
{
    /// <summary>
    /// Csv parser service
    /// </summary>
    public class ParserService : IDataReader
    {
        const char delimeter = ',';
        const string seriesTemplate = @"\d{4}";
        const string numberTemplate = @"\d{6}";

        readonly StreamReader _streamReader = new StreamReader(Directory.GetFiles("./Files/File/")[0]);
        readonly Func<string, bool>[] _constraintsTable =
            {
                x => new Regex(seriesTemplate).IsMatch(x),
                x => new Regex(numberTemplate).IsMatch(x)
            };

        string[] _currentLineValues;
        string _currentLine;

        public object GetValue(int i)
        {
            try
            {
                return _currentLineValues[i];
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Read()
        {
            if (_streamReader.EndOfStream) return false;

            _currentLine = _streamReader.ReadLine();
            _currentLineValues = _currentLine.Split(delimeter);

            var invalidRow = false;
            for (int i = 0; i < _currentLineValues.Length; i++)
            {
                if (!_constraintsTable[i](_currentLineValues[i]))
                {
                    invalidRow = true;
                    break;
                }
            }

            return !invalidRow || Read();
        }

        public int FieldCount
        {
            get { return 2; }
        }

        int IDataReader.Depth => throw new NotImplementedException();

        bool IDataReader.IsClosed => throw new NotImplementedException();

        int IDataReader.RecordsAffected => throw new NotImplementedException();

        object IDataRecord.this[string name] => throw new NotImplementedException();

        object IDataRecord.this[int i] => throw new NotImplementedException();

        public void Dispose()
        {
            _streamReader.Close();
        }

        void IDataReader.Close()
        {
            throw new NotImplementedException();
        }

        DataTable? IDataReader.GetSchemaTable()
        {
            throw new NotImplementedException();
        }

        bool IDataReader.NextResult()
        {
            throw new NotImplementedException();
        }

        bool IDataRecord.GetBoolean(int i)
        {
            throw new NotImplementedException();
        }

        byte IDataRecord.GetByte(int i)
        {
            throw new NotImplementedException();
        }

        long IDataRecord.GetBytes(int i, long fieldOffset, byte[]? buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        char IDataRecord.GetChar(int i)
        {
            throw new NotImplementedException();
        }

        long IDataRecord.GetChars(int i, long fieldoffset, char[]? buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        IDataReader IDataRecord.GetData(int i)
        {
            throw new NotImplementedException();
        }

        string IDataRecord.GetDataTypeName(int i)
        {
            throw new NotImplementedException();
        }

        DateTime IDataRecord.GetDateTime(int i)
        {
            throw new NotImplementedException();
        }

        decimal IDataRecord.GetDecimal(int i)
        {
            throw new NotImplementedException();
        }

        double IDataRecord.GetDouble(int i)
        {
            throw new NotImplementedException();
        }

        Type IDataRecord.GetFieldType(int i)
        {
            throw new NotImplementedException();
        }

        float IDataRecord.GetFloat(int i)
        {
            throw new NotImplementedException();
        }

        Guid IDataRecord.GetGuid(int i)
        {
            throw new NotImplementedException();
        }

        short IDataRecord.GetInt16(int i)
        {
            throw new NotImplementedException();
        }

        int IDataRecord.GetInt32(int i)
        {
            throw new NotImplementedException();
        }

        long IDataRecord.GetInt64(int i)
        {
            throw new NotImplementedException();
        }

        string IDataRecord.GetName(int i)
        {
            throw new NotImplementedException();
        }

        int IDataRecord.GetOrdinal(string name)
        {
            throw new NotImplementedException();
        }

        string IDataRecord.GetString(int i)
        {
            throw new NotImplementedException();
        }

        int IDataRecord.GetValues(object[] values)
        {
            throw new NotImplementedException();
        }

        bool IDataRecord.IsDBNull(int i)
        {
            throw new NotImplementedException();
        }
    }
}
