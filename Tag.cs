using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stand13
{
    public class Tag
    {
        private string value = "";

        /// <summary>
        /// Уникальный идентификатор на стороне клиента
        /// </summary>
        public int ClientHandle { get; private set; }

        /// <summary>
        /// Уникальный идентификатор на стороне сервера
        /// </summary>
        public int ServerHandle { get; private set; }

        /// <summary>
        /// Имя тэга описанного в ОРС сервере
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Текущее значение тэга
        /// </summary>
        public string Value
        {
            get { return value; }
            set
            {
                if (this.value != value)
                {
                    this.value = value;
                    ValueChanged?.Invoke(value);
                }
            }
        }

        /// <summary>
        /// Комментарий
        /// </summary>
        public string Comment { get; private set; }

        /// <summary>
        /// Текущее значение изменено
        /// </summary>
        public event Action<string> ValueChanged;

        /// <summary>
        /// Тэг ОРС сервера
        /// </summary>
        /// <param name="ClientHandle">Идентификатор на стороне клиента</param>
        /// <param name="Name">Имя тэга в ОРС сервере</param>
        /// <param name="Comment">Коментарий</param>
        public Tag(int ClientHandle, string Name, string Comment = "")
        {
            this.ClientHandle = ClientHandle;
            this.Name = Name;
            this.Comment = Comment;
        }
    }
}
