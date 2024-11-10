namespace dblw9
{
    public class Menu
    {
        public static string[] categoriesActions = {
            "Товары", // +
            "Склады",
            "Поставщики", // +
            "Работа со складом",
            "Аналитические данные",
        };
        public static string[] itemsInStorageStage1 =
        {
            "Найти элемент на складе",
            "Добавить элемент на склад",
            "Вывести все элементы со склада"
        };
        public static string[] itemsInStorageStage2 =
        {
            "Удалить элемент",
            "Редактировать элемент",
            
        };
        public static string[] supplierStage1 =
        {
            "Поиск", // +
            "Добавление", // +
            "Все поставщики", // +
        };
        public static string[] supplierStage2 =
        {
            "Удалить запись", // +
            "Редактировать запись" // +
        };
        public static string[] itemStage1 =
        {
            "Вывести все товары", // +
            "Найти товар", // +
            "Добавить запись о товаре", // +
        };
        public static string[] itemStage2 = {
            "Удалить запись о товаре", // +
            "Изменение записи о товаре", // +
        };
        public static string[] storageStage1 =
        {
            "Найти склад",
            "Добавить склад",
        };
        public static string[] storageStage2 =
        {
            "Удалить запись о складе",
            "Изменение записи о складе",
        };
        public static string[] customerStage1 = new[]
{
            "Вывести всех клиентов",
            "Добавить клиента",
            "Найти клиента"
        };

        public static string[] orderStage1 = new[]
        {
            "Вывести все заказы",
            "Добавить заказ",
            "Найти заказ"
        };

        public static string[] itemInOrderStage1 = new[]
        {
            "Вывести все элементы в заказах",
            "Добавить элемент в заказ",
            "Найти элемент в заказе"
        };
    }
}
