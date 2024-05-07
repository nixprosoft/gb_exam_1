using System;
using System.Linq;
using System.Text;

public class ExamAnswer
{

    // Преобразует строку с числом в массив цифр
    static void TrimStringsArray(ref String[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = array[i].Trim();
        }
    }

    static void AddDoubleQuotesToItems(ref String[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = "\"" + array[i] + "\"";
        }
    }

    /// <summary>
    /// Метод, фильующий массив слов по длинне
    /// </summary>
    /// <param name="array">исходный массив</param>
    /// <param name="max_lenght">лимит</param>
    /// <returns>Массив без слов, которые длиннее лимита</returns>
    static String[] FilterWordsArray(String[] array, int max_lenght = 3)
    {
        return array.Where(x => x.Length <= max_lenght).ToArray();
    }

    /// <summary>
    /// Функция плюрализации строк, зависящих от числа
    /// </summary>
    /// <param name="number">Число</param>
    /// <param name="words">Массив фраз для форм [одно, два, пять]</param>
    /// <returns>Одну из фраз второго аргумента</returns>
    static private String Pluralize(int number, String[] words)
    {
        int lastDigit = number % 10;
        if (lastDigit == 1)
        {
            return words[0];
        }
        else if (lastDigit >= 2 && lastDigit <= 4)
        {
            return words[1];
        }
        else
        {
            return words[2];
        }
    }

    static public void Main(string[] args)
    {
        String[] words_array;
        String[] result_array = [];
        String? input_string;
        int result_array_index = 0;
        const int word_lenght_limit = 3;    // лимит длинны слов
        bool interactive_mode = true;   // интерактивный режим подразумевает общение с пользователем

        if (args.Length > 0)
        {
            words_array = args;
            interactive_mode = false;   // если параметры переданы через командную строку, то интерактивный режим отключается
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Программа, которая из имеющегося массива строк формирует новый массив из строк, длина которых меньше, либо равна {0} {1}", word_lenght_limit, Pluralize(word_lenght_limit, ["символу", "символам", "символам"]));
            Console.Write("Введите слова через запятую и нажмите Enter: ");
            input_string = Console.ReadLine();
            if (input_string != null && input_string != "")
            {
                words_array = input_string.Split(',');
            }
            else
            {
                Console.WriteLine("Вы ввели пустую строку. Программа завершает работу.");
                return;
            }
        }

        // подгоняем размер результирующего массива
        Array.Resize(ref result_array, words_array.Length);

        // очищаем массив слов от пробелов
        TrimStringsArray(ref words_array);

        // фильтруем слова по длинне
        for (int i = 0; i < words_array.Length; i++)
        {
            if (words_array[i].Length <= word_lenght_limit)
            {
                result_array[result_array_index++] = words_array[i];
            }
        }

        // подгоняем размер результирующего массива
        Array.Resize(ref result_array, result_array_index);

        // выводим результат
        if (interactive_mode)
        {
            // формат человеческий
            Console.WriteLine("Вы ввели {0} {1}.", words_array.Length, Pluralize(words_array.Length, ["слово", "слова", "слов"]));
            Console.WriteLine("Осталось: {0} {1}.", result_array.Length, Pluralize(result_array.Length, ["слово", "слова", "слов"]));
            Console.Write("Результат: ");
            if (result_array.Length > 0)
            {
                for (int i = 0; i < result_array.Length; i++)
                {
                    Console.Write("\"{0}\" ", result_array[i]);
                }
            }
            else
            {
                Console.Write("пустой");
            }
        }
        else
        {
            // формат JSON
            AddDoubleQuotesToItems(ref result_array);
            Console.WriteLine("[{0}]", String.Join(", ", result_array));
        }

    }

}