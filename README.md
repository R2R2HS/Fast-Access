# Примеры

---

## 1. Доступ к свойству объекта

```csharp
public void AccessProperty()
{
    var propertyInfo = typeof(MyClass).GetProperty("MyProperty");
    var target = new MyClass();
    var value = FaAccess.Accessor_get(propertyInfo).Invoke(target);
    Console.WriteLine(value); // Выводит значение свойства MyProperty
}
```

- **`propertyInfo`** — информация о свойстве, полученная через рефлексию.  
- **`target`** — экземпляр класса, у которого запрашивается свойство.  
- **`FaAccess.Accessor_get`** — создает делегат для доступа к свойству.  
- **`Invoke(target)`** — вызывает делегат, чтобы получить значение свойства.  

---

## 2. Доступ к полю объекта

```csharp
public void AccessField()
{
    var fieldInfo = typeof(MyClass).GetField("MyField");
    var target = new MyClass();
    var value = FaAccess.Accessor_get(fieldInfo).Invoke(target);
    Console.WriteLine(value); // Выводит значение поля MyField
}
```

- **`fieldInfo`** — информация о поле, полученная через рефлексию.  
- **`target`** — экземпляр класса, у которого запрашивается поле.  
- **`FaAccess.Accessor_get`** — создает делегат для доступа к полю.  
- **`Invoke(target)`** — вызывает делегат, чтобы получить значение поля.  

---

## 3. Доступ к методу объекта

```csharp
public void AccessMethod()
{
    var methodInfo = typeof(MyClass).GetMethod("MyMethod");
    var target = new MyClass();
    var result = FaAccess.Accessor_invoke(methodInfo).Invoke(target, ["Hello"]);
    Console.WriteLine(result); // Выводит результат выполнения метода MyMethod
}
```

- **`methodInfo`** — информация о методе, полученная через рефлексию.  
- **`target`** — экземпляр класса, у которого вызывается метод.  
- **`FaAccess.Accessor_invoke`** — создает делегат для вызова метода.  
- **`Invoke(target, ["Hello"])`** — вызывает метод с аргументом `"Hello"`.  

---

## 4. Доступ к статическому свойству

```csharp
public void AccessStaticProperty()
{
    var propertyInfo = typeof(MyClass).GetProperty("StaticProperty");
    var value = FaAccess.Accessor_get(propertyInfo)!.Invoke(null);
    Console.WriteLine(value); // Выводит значение статического свойства StaticProperty
}
```

- **`propertyInfo`** — информация о статическом свойстве, полученная через рефлексию.  
- **`FaAccess.Accessor_get`** — создает делегат для доступа к свойству.  
- **`Invoke(null)`** — вызывает делегат. Для статических членов объект не требуется, поэтому передается `null`.  

---

## 5. Доступ к статическому полю

```csharp
public void AccessStaticField()
{
    var fieldInfo = typeof(MyClass).GetField("StaticField");
    var value = FaAccess.Accessor_get(fieldInfo)!.Invoke(null);
    Console.WriteLine(value); // Выводит значение статического поля StaticField
}
```

- **`fieldInfo`** — информация о статическом поле, полученная через рефлексию.  
- **`FaAccess.Accessor_get`** — создает делегат для доступа к полю.  
- **`Invoke(null)`** — вызывает делегат. Для статических членов объект не требуется, поэтому передается `null`.  

---

## 6. Доступ к статическому методу

```csharp
public void AccessStaticMethod()
{
    var methodInfo = typeof(MyClass).GetMethod("StaticMethod");
    var result = FaAccess.Accessor_invoke(methodInfo).Invoke(null, [42]);
    Console.WriteLine(result); // Выводит результат выполнения статического метода StaticMethod
}
```

- **`methodInfo`** — информация о статическом методе, полученная через рефлексию.  
- **`FaAccess.Accessor_invoke`** — создает делегат для вызова метода.  
- **`Invoke(null, [42])`** — вызывает статический метод с аргументом `42`.  

---

## Пример класса `MyClass`

```csharp
public class MyClass
{
    public string MyProperty { get; set; } = "PropertyValue";
    public string MyField = "FieldValue";

    public string MyMethod(string input) => $"Method called with: {input}";

    public static string StaticProperty { get; set; } = "StaticPropertyValue";
    public static string StaticField = "StaticFieldValue";

    public static string StaticMethod(int number) => $"StaticMethod called with: {number}";
}
```

---

Эти примеры готовы к использованию в вашем проекте. Вы можете адаптировать их под свои задачи.
