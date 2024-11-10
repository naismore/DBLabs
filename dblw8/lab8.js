// lab8.jsc


// Items
Items = (
    [
        {
            "_id": 1,
            name: "Apple iPhone 15 Pro 128GB Dual Sim Blue Titanium",
            unit: "шт",
            cost: 109999,
            id_supplier: 1,
            category: [{name: "smartphone"}, {name: "apple"}]
        },
        {
            "_id": 2,
            name: "Apple iPhone 13 128GB nanoSim/eSim Midnight",
            unit: "шт",
            cost: 59999,
            id_supplier: 1,
            category: ["smartphone", "apple"]
        },
        {
            "_id": 3,
            name: "Samsung Galaxy A15 LTE 4/128GB Dark Blue",
            unit: "шт",
            cost: 14999,
            id_supplier: 2,
            category: ["smartphone", "samsung"]
        },
        {
            "_id": 4,
            name: "HUAWEI nova 12s 8/256GB Black",
            unit: "шт",
            cost: 27999,
            id_supplier: 3,
            category: ["smartphone", "huawei"]
        },
        {
            "_id": 5,
            name: "Xiaomi Redmi Note 13 8/256GB Midnight Black",
            unit: "шт",
            cost: 20999,
            id_supplier: 4,
            category: ["smartphone", "xiaomi", "china"]
        },
        {
            "_id": 6,
            name: "Xiaomi Redmi 12 8/256GB Midnight Black",
            unit: "шт",
            cost: 12999,
            id_supplier: 4,
            category: ["smartphone", "xiaomi", "china"]
        },
    ]
)

// ItemsInStorage
ItemsInStorage = (
    [
        {
            "_id": 1,
            id_item: 1,
            id_storage: 1,
        },
        {
            "_id": 2,
            id_item: 2,
            id_storage: 2,
        },
        {
            "_id": 3,
            id_item: 3,
            id_storage: 3,
        },
        {
            "_id": 4,
            id_item: 4,
            id_storage: 4,
        },
        {
            "_id": 5,
            id_item: 5,
            id_storage: 5,
        },
        {
            "_id": 6,
            id_item: 6,
            id_storage: 6,
        },
    ]
)

// Storage

Storages = (
    [
        {
            "_id": 1,
            name: "ST1",
            adress: "Vladimir",
            phone_number: 123132131,
        },
        {
            "_id": 2,
            name: "ST2",
            adress: "Moscow",
        },
    ]
)


// Supplier

Suppliers = (
    [
        {
            "_id": 1,
            name: "Apple",
            contact_person_first_name: "Name1",
            contact_person_last_name: "Lastname1",
            phone_number: 123123123,
            adress: "New York City",
        },
        {
            "_id": 2,
            name: "Samsung",
            contact_person_first_name: "Name2",
            contact_person_last_name: "Lastname2",
            phone_number: 3213321321,
            adress: "Tokyo",
        },
        {
            "_id": 3,
            name: "Huawei",
            contact_person_first_name: "Name3",
            contact_person_last_name: "Lastname3",
            phone_number: 31312312,
            adress: "Huyung",
        },
        {
            "_id": 4,
            name: "Xiaomi",
            contact_person_first_name: "Name4",
            contact_person_last_name: "Lastname4",
            phone_number: 12341512,
            adress: "GungHuyung",
        },
])

// Orders
Orders = (
    [
        {
            "_id": 1,
            id_customer: 1,
        },
        {
            "_id": 2,
            id_customer: 2,
        },
        {
            "_id": 3,
            id_customer: 3,
        },
    ]
)

// ItemsInOrders
ItemsInOrder = (
    [
        {
            "_id": 1,
            id_item: 1,
            id_order: 1,
        },
        {
            "_id": 2,
            id_item: 4,
            id_order: 2,
        },
    
        {
            "_id": 3,
            id_item: 4,
            id_order: 3,
        },
    
    
    ]
)

// Customers

Customers = (
    [
        {
            "_id": 1,
            first_name: "Alan",
            last_name: "Walker",
            birth_date: "1972.06.28",
            email: "example1@gmail.com",
        },
        {
            "_id": 2,
            first_name: "Sam",
            last_name: "Mason",
            birth_date: "1985.06.28",
            email: "example2@gmail.com",
        },
        {
            "_id": 3,
            first_name: "Joe",
            last_name: "Biden",
            birth_date: "1979.06.28",
            email: "example3@gmail.com",
        },
    ]
)


// 1. Отобразить коллекции базы данных
// Запрос для отображения всех коллекций в базе данных
db.getCollectionNames();

// 2. Вставка записей
// 2.1 Вставка одной записи в коллекцию "склады"
db.storages.insertOne({
            "_id": 3,
            name: "ST3",
            adress: "Zelenograd",
            phone_number: 12441212,
        },
    )

// 2.2 Вставка нескольких записей в коллекцию "товары"
db.items.insertMany(Items);
db.itemsinstorage.insertMany(ItemsInStorage)
db.storages.insertMany(Storages)
db.suppliers.insertMany(Suppliers)
db.orders.insertMany(Orders)
db.itemsinorder.insertMany(ItemsInOrder)
db.customers.insertMany(Customers)

// 3. Удаление записей
// 3.1 Удаление одной записи по условию из коллекции "товары"
db.products.deleteOne({ _id: 1 });

// 3.2 Удаление нескольких записей по условию из коллекции "склады"
db.items.deleteMany({id_supplier: 1});

// 4. Поиск записей
// 4.1 Поиск по ID в коллекции "товары"
db.items.find({ _id: 4 });

// 4.2 Поиск записи по атрибуту первого уровня в коллекции "склады"
db.storages.find({ name: "ST1" });

// 4.3 Поиск записи по вложенному атрибуту (если есть вложенные структуры)
db.items.find({"category.name": "apple"})

// 4.4 Поиск записи по нескольким атрибутам (логический оператор AND)
db.items.find({ id_supplier: 1, "category.name": "apple"});

// 4.5 Поиск записи по одному из условий (логический оператор OR)
db.items.find({ $or: [{ price: { $gt: 10000 } }, { "category.name": "apple" }] });

// 4.6 Поиск с использованием оператора сравнения
db.items.find({ cost: { $lt: 100000 } });

// 4.7 Поиск с использованием двух операторов сравнения
db.items.find({ cost: { $gt: 10000, $lt: 50000 } });

// Теперь можно искать по значению в массиве
// 4.8 Поиск по значению в массиве
db.items.find({ category: "apple" });

// Пример поиска по количеству элементов в массиве
db.items.find ({category: {$size:3}})

// 4.10 Поиск записей без атрибута
db.storages.find ({phone_number: {$exists:true}})

// Обновление записей
// 5. Обновление записей
// Изменить значение атрибута у записи в коллекции "товары"
db.storages.updateOne(
    { _id: 3 },
    { $set: { phone_number: 31231321 } }
);

// Удалить атрибут у записи в коллекции "склады"
db.storages.updateOne(
    { _id: 1 },
    { $unset: { phone_number: 1 } }
);

// Добавить атрибут записи в коллекции "поставщики"
db.storages.updateOne(
    { _id: 2 },
    { $set: { phone_number: 121412422 } }
);