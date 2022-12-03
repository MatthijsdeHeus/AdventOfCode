from Utilities import *
import string

data = GetData(2022, 3)


def part1(input):
    counter = 0

    for datapoint in input:
        rucksack = list(datapoint)
        compLength = len(rucksack) / 2

        firstComp = rucksack[0:int(compLength)]
        secondComp = rucksack[-int(compLength):]

        commonLetter = getCommonLetter2Lists(firstComp, secondComp)

        counter += getLetterValue(commonLetter)

    return counter


def getCommonLetter2Lists(list1, list2):
    for secondCompItem in list1:
        if secondCompItem in list2:
            return secondCompItem


def getLetterValue(letter):
    if letter.isupper():
        return string.ascii_uppercase.index(letter) + 27
    else:
        return string.ascii_lowercase.index(letter) + 1


def part2(input):
    index = 0

    counter = 0

    while(index + 2 < len(input)):
        row1 = input[index]
        row2 = input[index + 1]
        row3 = input[index + 2]

        commonLetter = getCommonLetter3Lists(row1, row2, row3)

        counter += getLetterValue(commonLetter)

        index += 3

    return counter


def getCommonLetter3Lists(list1, list2, list3):
    common = []

    for secondListItem in list1:
        if secondListItem in list2:
            common.append(secondListItem)

    for item in common:
        if item in list3:
            return item


print(part1(data))
print(part2(data))
