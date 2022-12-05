import copy
from Utilities import *

data = GetData(2022, 5)

manualParsedTowers = [
["T", "P", "Z", "C", "S", "L", "Q", "N"],
["L", "P", "T", "V", "H", "C", "G"],
["D", "C", "F", "Z"],
["G", "W", "T", "D", "L", "M", "V", "C"],
["P", "W", "C"],
["P", "F", "J", "D", "C", "T", "S", "Z"],
["V", "W", "G", "B", "D"],
["N", "J", "S", "Q", "H", "W"],
["R", "C", "Q", "F", "S", "L", "V"]
]

tempTowers1 = copy.deepcopy(manualParsedTowers)
tempTowers2 = copy.deepcopy(manualParsedTowers)

def part1and2(input):
    for line in input:
        if len(list(line)) > 0:
            if list(line)[0] == "m":
                split = line.split()

                amount = int(split[1])
                fromTower = int(split[3])
                toTower = int(split[5])

                fromTower -= 1
                toTower -= 1

                move(fromTower, toTower, amount, tempTowers1)
                move2(fromTower, toTower, amount, tempTowers2)

    for x in range(9):
        print(tempTowers1[x][-1], end="")

    print("\n")

    for x in range(9):
        print(tempTowers2[x][-1], end="")


def move(fromTower, toTower, amount, towers):
    for x in range(amount):
        towers[toTower].append(towers[fromTower].pop())


def move2(fromTower, toTower, amount, towers):
    temp = []

    for x in range(amount):
        temp.append(towers[fromTower].pop())

    temp.reverse()

    towers[toTower].extend(temp)


part1and2(data)
