# Compiler and flags
CXX := g++
CXXFLAGS := -Wall -Wextra -std=c++11

# Libraries
LIBS := -lraylib -llua

# Directories
SRC_DIRS := $(shell find . -type d -not -path './assets')
OBJ_DIR := build
BIN_DIR := bin
ASSETS_DIR := assets

# Files
SRCS := $(wildcard $(addsuffix /*.cpp, $(SRC_DIRS)))
OBJS := $(patsubst %.cpp, $(OBJ_DIR)/%.o, $(SRCS))
TARGET := $(BIN_DIR)/stellarthing

# Rules
.PHONY: all clean run

all: $(TARGET)

# Create binary directory, compile sources, and link them
$(TARGET): $(OBJS) | $(BIN_DIR)
	$(CXX) $(CXXFLAGS) -o $@ $^ $(LIBS)
	cp -r $(ASSETS_DIR) $(BIN_DIR)

# Compile source files into object files
$(OBJ_DIR)/%.o: %.cpp | $(OBJ_DIR)
	@mkdir -p $(@D)
	$(CXX) $(CXXFLAGS) -c $< -o $@

# Create output directories if they don't exist
$(BIN_DIR) $(OBJ_DIR):
	mkdir -p $@

# Clean up build files
clean:
	rm -rf $(OBJ_DIR) $(BIN_DIR)

# Run the program
run: all
	./$(TARGET)
