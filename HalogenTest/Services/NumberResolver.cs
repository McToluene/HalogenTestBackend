using System.Collections.Generic;
using System.Linq;
using HalogenTest.Dtos;
using HalogenTest.Interfaces;

namespace HalogenTest.Services
{
    public class NumberResolver : INumberResolver
    {
        private readonly IUploadService _uploadService;

        public NumberResolver(IUploadService uploadService){
            _uploadService = uploadService;
        }

        public List<Result> Process(){
            var numbers = _uploadService.Read();
            if (numbers == null)
                return null;
            
            var divisibleByThree = "";
            var divisibleByFive = "";
            var divisibleBySeven = "";
            var oddNumbers = "";
            var evenNumbers = "";
            var total = 0;
            var smallestSubSet = "0";


            foreach (var number in numbers){
                if (number % 2 != 0){
                    oddNumbers += $" {number},";
                }
                else
                    evenNumbers += $" {number},";
                if (number % 3 == 0)
                    divisibleByThree += $" {number},";
                if (number % 5 == 0)
                    divisibleByFive += $" {number},";
                if (number % 7 == 0)
                    divisibleBySeven += $" {number},";

                total += number;
            }

            if (total > 100)
                smallestSubSet = SmallestSetGreaterThan100(numbers);

            return PrepareResponse(divisibleByThree, divisibleByFive, divisibleBySeven, oddNumbers, evenNumbers, Median(numbers), Mode(numbers),
                smallestSubSet);
        }

        private static string Median(List<int> numbers){
            return numbers
                .Skip((numbers.Count - 1) / 2)
                .Take(2 - numbers.Count % 2)
                .Average().ToString();
        }

        private static string Mode(List<int> numbers){
            return numbers
                .GroupBy(item => item)
                .OrderByDescending(group => group.Count())
                .First().Key.ToString();
        }

        private static string SmallestSetGreaterThan100(List<int> numbers){
            var total = 0;
            var set = "";
            foreach (var number in numbers){
                total += number;
                set += $" {number},";
                if (total > 100)
                    return set;
            }
            return null;
        }

        private static List<Result> PrepareResponse(string divisibleByThree, string divisibleByFive,
            string divisibleBySeven, string oddNumbers, string evenNumbers, string median, string mode, string smallestSubset){
            var results = new List<Result>{
                new(){
                    Data = divisibleByThree,
                    Title = "Divisible by 3"
                },
                new(){
                    Data = divisibleByFive,
                    Title = "Divisible by 5"
                },
                new(){
                    Data = divisibleBySeven,
                    Title = "Divisible by 7"
                },
                new(){
                    Data = oddNumbers,
                    Title = "Odd numbers"
                },
                new(){
                    Data = evenNumbers,
                    Title = "Even numbers"
                },
                new(){
                    Data = median,
                    Title = "Median"
                },
                new(){
                    Data = mode,
                    Title = "Mode"
                },
                new(){
                    Data = smallestSubset,
                    Title = "Smallest set of numbers in count that if added together would be greater than 100"
                }
            };
            return results;
        }
    }
}