digits = {'zero' : 0, 'one' : 1, "two" : 2, 'three' : 3, 'four' : 4, 'five' : 5, 'six' : 6, 'seven' : 7, 'eight' : 8, 'nine' : 9}

linie = []
suma = 0

for i in range(1000):
    linia = input().strip()
    firstDigit = -1
    secondDigit = -1

    for i in range(len(linia)):
        if firstDigit != -1:
            break
        if ord(linia[i]) >= ord('0') and ord(linia[i]) <= ord('9'):
            firstDigit = int(linia[i])
        else:
            for digit_string in digits:
                str_len = len(digit_string)
                if linia[i : i + str_len] == digit_string:
                    firstDigit = digits[digit_string]
            

    for i in range(len(linia) - 1, -1, -1):
        if secondDigit != -1:
            break

        if ord(linia[i]) >= ord('0') and ord(linia[i]) <= ord('9'):
            secondDigit = int(linia[i])
        else:
            for digit_string in digits:
                str_len = len(digit_string)
                if linia[i - str_len + 1: i + 1] == digit_string:
                    secondDigit = int(digits[digit_string]) 
    suma += 10 * firstDigit + secondDigit


    
print(suma)