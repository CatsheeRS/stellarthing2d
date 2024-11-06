import sys
import subprocess
import argparse

def __init__():
    parser = argparse.ArgumentParser(prog="cat++", description="make c++ greg again")
    parser.add_argument("install-gmake", help="Setups Make stuff for the project")
    subparsers = parser.add_subparsers(dest='mode', help='Choose a mode to run')
    subparsers.required = True

    _ = subparsers.add_parser("install-gmake", help="Sets up Make Makefile files")
    _ = subparsers.add_parser("clean", help="Remove all binaries and generated projects")

    args = parser.parse_args()

    if args.mode == "install-gmake":
        install_gmake()
        return
    
    if args.mode == "clean":
        clean()
        return

def install_gmake():
    subprocess.run(["./lib/premake-5.0.0-beta2-linux/premake5", "gmake2", "--file=build.lua"])

def clean():
    subprocess.run(["./lib/premake-5.0.0-beta2-linux/premake5", "clean", "--file=build.lua"])