suma = 0
for i in range(1000):
    line = list(input())
    for c in line:
        if(ord(c) <= ord('9') and ord(c) >= ord('0')):
            suma += 10 * int(c)
            break
    for c in line[::-1]:
        if(ord(c) <= ord('9') and ord(c) >= ord('0')):
            suma += int(c)
            break
print(suma)