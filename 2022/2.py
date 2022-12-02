from Utilities import *

data = GetData(2022, 2)

def part1(input):
    sum = 0

    for round in input:
        opponent = round.split()[0]
        own = round.split()[1]

        if own == "X":
            sum += 1

            if opponent == "A":
                sum += 3
            if opponent == "B":
                sum += 0
            if opponent == "C":
                sum += 6

        if own == "Y":
            sum += 2

            if opponent == "A":
                sum += 6
            if opponent == "B":
                sum += 3
            if opponent == "C":
                sum += 0

        if own == "Z":
            sum += 3

            if opponent == "A":
                sum += 0
            if opponent == "B":
                sum += 6
            if opponent == "C":
                sum += 3

    return sum


def part2(input):
    newData = []

    for round in input:
        opponent = round.split()[0]
        roundStatus = round.split()[1]

        if opponent == "A":
            if roundStatus == "X":
                newData.append(opponent + " Z")
            if roundStatus == "Y":
                newData.append(opponent + " X")
            if roundStatus == "Z":
                newData.append(opponent + " Y")

        if opponent == "B":
            if roundStatus == "X":
                newData.append(opponent + " X")
            if roundStatus == "Y":
                newData.append(opponent + " Y")
            if roundStatus == "Z":
                newData.append(opponent + " Z")

        if opponent == "C":
            if roundStatus == "X":
                newData.append(opponent + " Y")
            if roundStatus == "Y":
                newData.append(opponent + " Z")
            if roundStatus == "Z":
                newData.append(opponent + " X")

    return part1(newData)


print(part1(data))
print(part2(data))
