<template>
  <div class="home">
    <navigation-bar />
    <v-container ma-5>
        <v-layout justify-flex-start align-flex-bottom wrap>
            <v-flex xs12 sm12 md3 pa-1 v-for="keep in keeps" :key="keep.id">
              <keeps :keep="keep" />
            </v-flex>
        </v-layout>
        <v-layout justify-center>
          <v-btn color="cyan lighten-3" class="white--text" @click="getKeeps(keeps[keeps.length-1].id)">load more</v-btn>
        </v-layout>
    </v-container>
  </div>
</template>

<script>
import NavigationBar from "@/components/NavigationBar";
import Keeps from "@/components/Keeps";
export default {
  name: "home",
  props: ["browsing"],
  mounted() {
    //blocks users not logged in
    if (!this.$store.state.user.id) {
      if (this.browsing) {
        console.log("props as bools work")
        return this.$store.dispatch("getKeeps", "first");
      }
    }
    this.$store.dispatch("getKeeps", "first");
  },
  computed: {
    keeps() {
      return this.$store.state.keeps;
    }
  },
  components: {
    NavigationBar,
    Keeps
  },
  methods: {
    getKeeps(id) {
      this.$store.dispatch("getKeeps", id)
    }
  }
};
</script>