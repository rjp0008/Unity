package main

import (
	"bytes"
	"fmt"
	"net"
"encoding/binary"
)

func main() {
	addr, _ := net.ResolveUDPAddr("udp", ":2000")
	sock, _ := net.ListenUDP("udp", addr)

	i := 0
	for {
		i++
		buf := make([]byte, 1024)
		rlen, source, err := sock.ReadFromUDP(buf)
		if err != nil {
			fmt.Println(err)
		}
		fmt.Println(string(buf[0:rlen]))
		var xpos float64
		var ypos float64
reader := bytes.NewReader(buf[:8])
binary.Read(reader, binary.LittleEndian,&xpos)
reader = bytes.NewReader(buf[8:rlen])
binary.Read(reader, binary.LittleEndian,&ypos)
fmt.Println(xpos)
fmt.Println(ypos)
		sock.WriteToUDP(bytes.ToUpper(buf[0:rlen]), source)
		//go handlePacket(buf, rlen)
	}
}
