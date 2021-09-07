using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stand13
{
    public static class Tags
    {
        /// <summary>
        /// Заранее определенные тэги для работы станка
        /// </summary>
        private static Tag[] Items = new Tag[]
        {
            new Tag(1, "PosNum", "Позиция стенда" ), // 0 - не выбрана; 1 - первая; 2 - вторая; 3 - обе
            new Tag(2, "QErr", "Общая ошибка" ), // 0 - OK, 1 - Error
            new Tag(3, "QbManAut", "Режим работы" ), // 0 - Manual, 1 - Auto
            new Tag(4, "QbRun", "Состояние" ), // 0 - Stop, 1 - Run
            new Tag(5, "bCntReset", "Сброс счетчика пробега" ), // 1 - Reset , сбрасывается контроллером
            new Tag(6, "bReset", "Сброс ошибок контроллера" ), // 1 - Reset , сбрасывается контроллером
            new Tag(7, "bStart", "Запуск или продолжение испытания" ), // 1 - Start , сбрасывается контроллером
            new Tag(8, "bStop", "Останов испытания" ), // 1 - Stop , сбрасывается контроллером
            new Tag(9, "Dist0", "???" ),
            new Tag(10, "P_1s", "Метроном в 1 сек" ),

            new Tag(101, "Pos1\\DR_max", "Динамический радиус макс." ),
            new Tag(102, "Pos1\\DR_min", "Динамический радиус мин." ),
            new Tag(103, "Pos1\\QDRmin_Err", "Достигнут минимальный радиус" ), // 1 - Reached
            new Tag(104, "Pos1\\QDamage_Err", "Разрушение покрышки"), // 1 - Damage
            new Tag(105, "Pos1\\QEmSTOP_Err", "Аварийный останов"), // 1 - Emergency STOP
            new Tag(106, "Pos1\\QGuard_Err", "Ограждение"), // 1 - Guard open
            new Tag(107, "Pos1\\QfDR", "Измеренный динамический радиус"),
            new Tag(108, "Pos1\\QfLoad", "Измеренная нагрузка"),
            new Tag(109, "Pos1\\QfMileage", "Актуальный пробег"),
            new Tag(110, "Pos1\\QfVelocity", "Измеренная скорость"),
            new Tag(111, "Pos1\\SP_Load", "Задать нагрузку"),
            new Tag(112, "Pos1\\SP_Velocity", "Задать скорость"),
            new Tag(113, "Pos1\\Temperature", "Температура"),

            new Tag(201, "Pos2\\DR_max", "Динамический радиус макс."),
            new Tag(202, "Pos2\\DR_min", "Динамический радиус мин."),
            new Tag(203, "Pos2\\QDRmin_Err", "Достигнут минимальный радиус"), // 1 - Reached
            //new Tag( 204, "Pos2\\QDamage_Err", "Разрушение покрышки"), // 1 - Damage
            new Tag(205, "Pos2\\QEmSTOP_Err", "Аварийный останов"), // 1 - Emergency STOP
            new Tag(206, "Pos2\\QGuard_Err", "Ограждение"), // 1 - Guard open
            new Tag(207, "Pos2\\QfDR", "Измеренный динамический радиус"),
            new Tag(208, "Pos2\\QfLoad", "Измеренная нагрузка"),
            new Tag(209, "Pos2\\QfMileage", "Актуальный пробег"),
            new Tag(210, "Pos2\\QfVelocity", "Измеренная скорость"),
            new Tag(211, "Pos2\\SP_Load", "Задать нагрузку"),
            new Tag(212, "Pos2\\SP_Velocity", "Задать скорость" ),
            new Tag(213, "Pos2\\Temperature", "Температура" ),

            new Tag(300, "Pos1\\bUndocked", "Отведена"),
            new Tag(301, "Pos1\\bUndocke", "Отводится"),
            new Tag(302, "Pos1\\bDocked", "Подведена"),
            new Tag(303, "Pos1\\bDocke", "Подводится"),
            new Tag(304, "Pos2\\bUndocked", "Отведена"),
            new Tag(305, "Pos2\\bUndocke", "Отводится"),
            new Tag(306, "Pos2\\bDocked", "Подведена"),
            new Tag(307, "Pos2\\bDocke", "Подводится")
        };

        /// <summary>
        /// Данные тэгов изменены
        /// </summary>
        /// <param name="TransactionID">Идентификатор транзакции</param>
        /// <param name="NumItems">Количество тегов с измененными данными</param>
        /// <param name="ClientHandles">Идентификаторы тэгов заданные клиентом</param>
        /// <param name="ItemValues">Значение тэгов</param>
        /// <param name="Qualities">Качество тэгов</param>
        /// <param name="TimeStamps">Время получение данных</param>
        private static void OPCGroupDataChange(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps)
        {
            for (int i = 1; i < NumItems + 1; i++)
            {
                GetTag((int)ClientHandles.GetValue(i)).Value = ItemValues.GetValue(i).ToString();
            }
        }

        /// <summary>
        /// Запись выполнена
        /// </summary>
        /// <param name="TransactionID"></param>
        /// <param name="NumItems"></param>
        /// <param name="ClientHandles"></param>
        /// <param name="Errors"></param>
        private static void OPCGroupAsyncWriteComplete(int TransactionID, int NumItems, ref Array ClientHandles, ref Array Errors)
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Чтение выполнено
        /// </summary>
        /// <param name="TransactionID"></param>
        /// <param name="NumItems"></param>
        /// <param name="ClientHandles"></param>
        /// <param name="ItemValues"></param>
        /// <param name="Qualities"></param>
        /// <param name="TimeStamps"></param>
        /// <param name="Errors"></param>
        private static void OPCGroupAsyncReadComplete(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps, ref Array Errors)
        {
            throw new NotImplementedException();
        }

        public static Tag GetTag(int ClientHandle)
        {
            return Items.First(x => x.ClientHandle == ClientHandle);
        }

        public static Tag GetTag(string Name)
        {
            return Items.First(x => x.Name == Name);
        }
    }
}
