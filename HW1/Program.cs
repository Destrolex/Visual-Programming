using System;
using System.Collections.Generic;

// Интерфейс для уведомлений
public interface INotifier
{
    void Notify(string message);
}

// Класс аккаунта пользователя
public class Account
{
    private double _balance;
    private List<INotifier> _notifiers;

    public Account(double initialBalance)
    {
        _balance = initialBalance;
        _notifiers = new List<INotifier>();
    }

    // Метод для добавления уведомления
    public void AddNotifier(INotifier notifier)
    {
        _notifiers.Add(notifier);
    }

    // Метод для удаления уведомления
    public void RemoveNotifier(INotifier notifier)
    {
        _notifiers.Remove(notifier);
    }

    // Метод для добавления к балансу с уведомлением
    public void AddBalance(double amount)
    {
        _balance += amount;
        NotifyAll($"Added {amount} to balance. New balance: {_balance}");
    }

    // Метод для вычитания из баланса с уведомлением
    public void DeductBalance(double amount)
    {
        _balance -= amount;
        NotifyAll($"Deducted {amount} from balance. New balance: {_balance}");
    }

    // Приватный метод для отправки уведомлений всем зарегистрированным уведомителям
    private void NotifyAll(string message)
    {
        foreach (var notifier in _notifiers)
        {
            notifier.Notify(message);
        }
    }

    // Метод для получения текущего баланса
    public double GetBalance()
    {
        return _balance;
    }
}

// Пример уведомления по электронной почте
public class EmailNotifier : INotifier
{
    public void Notify(string message)
    {
        Console.WriteLine($"Email notification: {message}");
    }
}

// Пример уведомления по SMS
public class SMSNotifier : INotifier
{
    public void Notify(string message)
    {
        Console.WriteLine($"SMS notification: {message}");
    }
}

class Program
{
    static void Main()
    {
        // Создаем аккаунт с начальным балансом
        Account account = new Account(1000);

        // Добавляем уведомители
        INotifier emailNotifier = new EmailNotifier();
        INotifier smsNotifier = new SMSNotifier();

        account.AddNotifier(emailNotifier);
        account.AddNotifier(smsNotifier);

        // Изменяем баланс и проверяем уведомления
        account.AddBalance(500);
        account.DeductBalance(200);

        // Удаляем уведомление по SMS
        account.RemoveNotifier(smsNotifier);

        // Еще одно изменение баланса
        account.AddBalance(1000);

        // Выводим текущий баланс
        Console.WriteLine($"Current balance: {account.GetBalance()}");
    }
}