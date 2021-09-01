
using System;
using System.Collections.Generic;
using System.Text;

namespace CbtcTcp
{
    class MessageEntity
    {
    public MessageEntity()
        {
            this.Id = System.Threading.Interlocked.Increment(ref m_Counter);
            this.dateTime = DateTime.Now;
        }
        private static int m_Counter = 0;

        public int Id { get; set; }
        public string message { get; set; }
        public string ip { get; set; }
        public DateTime dateTime { get; set; }

    }
}
