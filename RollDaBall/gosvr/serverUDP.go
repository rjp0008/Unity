package main

import (
	"bytes"
	"encoding/binary"
	"fmt"
	"net"
)

func main() {
	addr, _ := net.ResolveUDPAddr("udp", ":2000")
	sock, _ := net.ListenUDP("udp", addr)

	i := 0
	for {
		i++
		buf := new(bytes.Buffer)
		p := player{}
		rlen, source, err := sock.ReadFrom(buf)
		if err != nil {
			fmt.Println(err)
		}
		binary.Write(p,binary.BigEndian,buf[:rlen])
		sock.WriteToUDP(buf[0:rlen], source)
		//go handlePacket(buf, rlen)
	}
}

type player struct {
	xPos, yPos, xSpeed, ySpeed float32
	id, packet                 int32
	version, checksum          byte
}
