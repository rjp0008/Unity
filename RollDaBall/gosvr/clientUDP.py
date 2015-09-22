import sys
import socket

UDP_IP="127.0.0.1"
UDP_PORT="8081"

sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
sock.sendto(toSendMsg, (UDP_IP, UDP_PORT))
 
while True:
    data, addr = sock.recvfrom(100)
    #or data, (UDP_IP, UDP_PORT) = sock.recvfrom(100)
    #100 is buffer size
 
endTime = time.clock()
 
print "Request ID: ", data[2], data[3]
print "Response/Answer: ", data[4:]
 
#print out the round trip time
totalTimeSpent = endTime - startTime
print "The total round trip time in seconds was ", totalTimeSpent