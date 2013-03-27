using System;
namespace ILog.Model
{
    /// <summary>
    /// ʵ���ࣺԭ����
    /// </summary>
    public class ILogOriginal
    {
        #region Model
        private long _io_id;
        private long _userid;
        private string _io_content;
        private string _io_ip;
        private DateTime _intime;
        private bool _io_haspic;
        private int _is_id;
        private int _cw_type;
        private int _io_spreadnum;
        private int _io_commentnum;

        private string _nickname;

        private int _vi_memberlevel;

        /// <summary>
        /// ������ʶ 1
        /// </summary>
        public long io_id
        {
            set { _io_id = value; }
            get { return _io_id; }
        }
        /// <summary>
        /// Vip���е�userid
        /// </summary>
        public long userid
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string io_content
        {
            set { _io_content = value; }
            get { return _io_content; }
        }
        /// <summary>
        /// ����IP
        /// </summary>
        public string io_ip
        {
            set { _io_ip = value; }
            get { return _io_ip; }
        }
        /// <summary>
        /// Ĭ����getdate()
        /// </summary>
        public DateTime intime
        {
            set { _intime = value; }
            get { return _intime; }
        }
        /// <summary>
        /// �Ƿ���ͼƬ
        /// </summary>
        public bool io_haspic
        {
            set { _io_haspic = value; }
            get { return _io_haspic; }
        }
        /// <summary>
        /// ��Դid
        /// </summary>
        public int is_id
        {
            set { _is_id = value; }
            get { return _is_id; }
        }
        /// <summary>
        /// Ĭ����0 ԭ�� 1����
        /// </summary>
        public int cw_type
        {
            set { _cw_type = value; }
            get { return _cw_type; }
        }

        /// <summary>
        /// ת������ Ĭ����0
        /// </summary>
        public int io_spreadnum
        {
            set { _io_spreadnum = value; }
            get { return _io_spreadnum; }
        }

        /// <summary>
        /// ���۴��� Ĭ����0
        /// </summary>
        public int io_commentnum
        {
            set { _io_commentnum = value; }
            get { return _io_commentnum; }
        }

        #endregion Model

    }
}

