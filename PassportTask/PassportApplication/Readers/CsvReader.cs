﻿using System.Data;
using System.Text.RegularExpressions;

namespace PassportApplication.Readers
{
    /// <summary>
    /// Implements IDataReader
    /// </summary>
    public class CsvReader : IDataReader
    {
        const char delimeter = ',';
        const string seriesTemplate = @"\d{4}";
        const string numberTemplate = @"\d{6}";

        readonly StreamReader _streamReader;

        readonly Func<string, bool>[] _constraintsTable =
            {
                x => new Regex(seriesTemplate).IsMatch(x),
                x => new Regex(numberTemplate).IsMatch(x)
            };

        string[] _currentLineValues;
        string? _currentLine;

        /// <summary>
        /// CsvReader constructor
        /// </summary>
        /// <param name="FilePath">Path of the csv file</param>
        public CsvReader(string FilePath)
        {
            _streamReader = new StreamReader(FilePath);
        }

        /// <summary>
        /// Implements IDataReader.GetValue(int i)
        /// </summary>
        /// <param name="i">Index of the value</param>
        /// <returns></returns>
        public object GetValue(int i)
        {
            return _currentLineValues[i];
        }

        /// <summary>
        /// Implements IDataReader.Read()
        /// </summary>
        /// <returns></returns>
        public bool Read()
        {
            if (_streamReader.EndOfStream) return false;

            _currentLine = _streamReader.ReadLine();
            if (_currentLine == null) return Read();

            _currentLineValues = _currentLine.Split(delimeter);
            if (_currentLineValues.Length != FieldCount) return Read();

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

        /// <summary>
        /// Implements IDataReader.FieldCount
        /// </summary>
        public int FieldCount
        {
            get { return 2; }
        }

        int IDataReader.Depth => throw new NotImplementedException();

        bool IDataReader.IsClosed => throw new NotImplementedException();

        int IDataReader.RecordsAffected => throw new NotImplementedException();

        object IDataRecord.this[string name] => throw new NotImplementedException();

        object IDataRecord.this[int i] => throw new NotImplementedException();

        /// <summary>
        /// Implements IDataReader.Dispose()
        /// </summary>
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
