from Utilities import *

data = GetData(2022, 1)

def part1():
    elfList = [0]

    index = 0

    for datapoint in data:
        if len(datapoint) == 0:
            elfList.append(0)
            index += 1
        else:
            elfList[index] += int(datapoint)

    print(max(elfList))

    return elfList

def part2():
    elfList = part1()

    elfList.sort(reverse=True)
    print(elfList[0] + elfList[1] + elfList[2])

part2()