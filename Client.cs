using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientTcp
{
    internal class Program
    {
        static void Main(string[] args) // Клиент
        {
            const string IP = "127.0.0.1"; // Локальная машина (IP version 4.0)
            const int PORT = 8080; // Порт

            var tcpEndPoint = new IPEndPoint(IPAddress.Parse(IP), PORT); // Конечная Точка подключения

            var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); // Сокет (Дверка в которую можно заходить) Через которую будет проводится соединение

            Console.Write("Введите сообщение: ");
            var message = Console.ReadLine(); // Ввод отправляемых данных на сервер 

            var data = Encoding.UTF8.GetBytes(message); // Перекодировка string в byte

            tcpSocket.Connect(tcpEndPoint); // Подключение к конечной точки.

            tcpSocket.Send(data); // Отправление данных.

            var buffer = new byte[256]; // Буфер, куда будем принимать данные, где 256 это максимальное количество принимаемых байт.
            var size = 0; // Количество реальных полученных байт.
            var answer = new StringBuilder(); // StringBuilder позволяет удобно форматировать данные.

            do
            {
                size = tcpSocket.Receive(buffer); // Получение байтов и помещение их в size.
                answer.Append(Encoding.UTF8.GetString(buffer, 0, size)); // Мы берем и из большого сообщения (answer) и мы эту строку из 256 символов берем перекодировали добавили в итогувую строку.

            } while (tcpSocket.Available > 0); // Если количество созданных конкретных подключений клиента будет > 0 будет повторятся цикл.

            Console.WriteLine(answer.ToString());

            tcpSocket.Shutdown(SocketShutdown.Both); // Двух стороннее закрытие, закрываем у клиента и у сервера.
            tcpSocket.Close(); // Закрыть 

            Console.ReadLine();
        }
    }
}
