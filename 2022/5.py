from Utilities import *

data = GetData(2022, 5)

towers = [
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

def part1(input):
    for line in input:
        if len(list(line)) > 0:
            if list(line)[0] == "m":
                split = line.split()
                amount = split[1]
                fromTower = split[3]
                toTower = split[5]

                move2(int(fromTower), int(toTower), int(amount))

    for x in range(9):
        print(towers[x][-1])

def move(fromTower, toTower, amount):
    fromTower -= 1
    toTower -= 1

    for x in range(amount):
        towers[toTower].append(towers[fromTower].pop())

def move2(fromTower, toTower, amount):
    fromTower -= 1
    toTower -= 1

    temp = []

    for x in range(amount):
        temp.append(towers[fromTower].pop())

    temp.reverse()

    towers[toTower].extend(temp)

print(part1(data))