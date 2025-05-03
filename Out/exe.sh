aarch64-linux-gnu-as -mcpu=cortex-a57 program.s -o program.o
aarch64-linux-gnu-ld program.o -o program
qemu-aarch64 ./program