import socket
import sys
from struct import *
from collections import namedtuple
from array import array
import time
    
UDP_IP = "127.0.0.1"
UDP_PORT = 2000
    
sock = socket.socket(socket.AF_INET, # Internet
                         socket.SOCK_DGRAM) # UDP
sock.bind((UDP_IP, UDP_PORT))
players = {}
while True:
       data, addr = sock.recvfrom(1024) # buffer size is 1024 bytes
       #player = namedtuple('xpos','ypos','xspeed','yspeed','id','sequence','v','checksum')
       try:
           player = unpack('ffffIIBB',data)
       except:
           continue
       players.setdefault(player[4],player)
       if players[player[4]][5] < player[5]:
           print player[5]
           players[player[4]] = player
       else:
           sock.sendto('',addr)
           continue
       output = array('c')
       for _, value in players.items():
           for char in pack('ffffIIBB',value[0],value[1],value[2],value[3],value[4],value[5],value[6],value[7]):
               output.append(char)
           for char in pack('ffffIIBB',-value[0],-value[1],value[2],value[3],1,value[5],value[6],value[7]):
               output.append(char)
       sock.sendto(output,addr)
       
       
