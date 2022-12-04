from Utilities import *

data = GetData(2022, 4)

def part1and2(input):
    containsCounter = 0
    overlapCounter = 0

    for datapoint in input:
        elf1 = datapoint.split(",")[0]
        elf2 = datapoint.split(",")[1]

        elf1Min = int(elf1.split("-")[0])
        elf1Max = int(elf1.split("-")[1])

        elf2Min = int(elf2.split("-")[0])
        elf2Max = int(elf2.split("-")[1])

        if elfContainsOther(elf1Min, elf1Max, elf2Min, elf2Max) is True:
            containsCounter += 1

        if rangesOverlap(elf1Min, elf1Max, elf2Min, elf2Max) is True:
            overlapCounter += 1

    print(containsCounter)
    print(overlapCounter)

def elfContainsOther(elf1Min, elf1Max, elf2Min, elf2Max):
    if elf1Min <= elf2Min and elf1Max >= elf2Max:
        return True

    if elf2Min <= elf1Min and elf2Max >= elf1Max:
        return True

def rangesOverlap(elf1Min, elf1Max, elf2Min, elf2Max):
    if elf1Min <= elf2Min and elf1Max >= elf2Min:
        return True

    if elf2Min <= elf1Min and elf2Max >= elf1Min:
        return True

part1and2(data)