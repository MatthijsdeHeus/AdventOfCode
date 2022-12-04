from Utilities import *

data = GetData(2022, 4)

def part1(input):
    counter = 0

    for datapoint in  input:
        elf1 = datapoint.split(",")[0]
        elf2 = datapoint.split(",")[1]

        if elfContainsOther(elf1, elf2) is True:
            counter += 1

    return counter

def elfContainsOther(elf1, elf2):
    elf1Min = int(elf1.split("-")[0])
    elf1Max = int(elf1.split("-")[1])

    elf2Min = int(elf2.split("-")[0])
    elf2Max = int(elf2.split("-")[1])


    if elf1Min <= elf2Min and elf1Max >= elf2Max:
        return True

    if elf2Min <= elf1Min and elf2Max >= elf1Max:
        return True

def part2(input):
    counter = 0

    for datapoint in input:
        elf1 = datapoint.split(",")[0]
        elf2 = datapoint.split(",")[1]

        if rangesOverlap(elf1, elf2) is True:
            counter += 1

    return counter

def rangesOverlap(elf1, elf2):
    elf1Min = int(elf1.split("-")[0])
    elf1Max = int(elf1.split("-")[1])

    elf2Min = int(elf2.split("-")[0])
    elf2Max = int(elf2.split("-")[1])

    if elf1Min <= elf2Min and elf1Max >= elf2Min:
        return True

    if elf2Min <= elf1Min and elf2Max >= elf1Min:
        return True


print(part1(data))
print(part2(data))