using LinkarClient;
using LinkarCommon;
using System.Collections.Generic;

namespace LINKARDEMO
{
    public class Customer 
    {
        #region Properties

        #region CODE

        private string _Code;
        [MVProperty(AttributeNumber = 0, DictionaryName = "@ID")]
        public string Code
        {
            get
            {
                return _Code;
            }
            set
            {
                _Code = value;
            }
        }

        #endregion

        #region NAME

        private string _Name;
        [MVProperty(AttributeNumber = 1, DictionaryName = "NAME")]
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }

        #endregion

        #region ADDRESS

        private string _Address;
        [MVProperty(AttributeNumber = 2, DictionaryName = "ADDRESS")]
        public string Address
        {
            get
            {
                return _Address;
            }
            set
            {
                _Address = value;
            }
        }

        #endregion

        #region PHONE

        private string _Phone;
        [MVProperty(AttributeNumber = 3, DictionaryName = "PHONE")]
        public string Phone
        {
            get
            {
                return _Phone;
            }
            set
            {
                _Phone = value;
            }
        }

        #endregion

        #endregion

        #region Contructors

        /// <summary>
        /// Build the object
        /// </summary>
        public Customer()
        {

        }

        #endregion
    }

    public class Customers : List<Customer>
    {
        public Customers()
        {

        }
    }
}
