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

        if commonLetter.isupper():
            counter += string.ascii_uppercase.index(commonLetter) + 27
        else:
            counter += string.ascii_lowercase.index(commonLetter) + 1

    return counter


def getCommonLetter2Lists(list1, list2):
    for secondCompItem in list1:
        if secondCompItem in list2:
            return secondCompItem


def part2(input):
    index = 0

    counter = 0

    while(index + 2 < len(input)):
        row1 = input[index]
        row2 = input[index + 1]
        row3 = input[index + 2]

        commonLetter = getCommonLetter3Lists(row1, row2, row3)

        if commonLetter.isupper():
            counter += string.ascii_uppercase.index(commonLetter) + 27
        else:
            counter += string.ascii_lowercase.index(commonLetter) + 1

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
