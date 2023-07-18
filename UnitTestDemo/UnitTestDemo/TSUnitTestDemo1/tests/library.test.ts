import { describe } from "node:test";
import { it } from "node:test";
import getData = require("../wwwroot/js/library.js");
import assert = require("assert");

describe("TestSuit1", function () {
    it("getData", function () {
        assert.ok(true, getData(2));
    })
})