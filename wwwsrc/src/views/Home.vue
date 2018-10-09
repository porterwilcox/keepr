<template>
  <div class="home">
    <navigation-bar />
    <v-container mt-5 pb-5>
        <v-layout justify-flex-start align-flex-bottom wrap>
            <v-flex xs12 sm12 md3 pa-1 v-for="keep in keeps" :key="keep.id">
              <keeps :keep="keep" />
            </v-flex>
        </v-layout>
        <v-layout v-if="lastGetCount == 28" justify-center pt-5 mt-5>
          <v-btn round color="cyan lighten-3" class="white--text" @click="getKeeps(keeps[keeps.length-1].id)">load more</v-btn round>
        </v-layout>
        <v-layout v-else justify-center pt-5 mt-5>
          <v-chip v-model="caughtUp" color="cyan" outline close>You're all caught up!</v-chip>
        </v-layout>
    </v-container>
        <bottom-bar />
  </div>
</template>

<script>
import NavigationBar from "@/components/NavigationBar";
import BottomBar from "@/components/BottomBar";
import Keeps from "@/components/Keeps";
export default {
  name: "home",
  props: ["browsing"],
  data() {
    return {
      caughtUp: true
    }
  },
  mounted() {
    //blocks users not logged in
    if (this.browsing) {
      return this.$store.dispatch("getKeeps", "first");
    }
    if (!this.$store.state.user.id) {
      this.$router.push({ name: "login" });      
    }
    else {
      this.$store.dispatch("getKeeps", "first");
    }
  },
  computed: {
    keeps() {
      return this.$store.state.keeps;
    },
    lastGetCount() {
      return this.$store.state.lastGetCount
    }
  },
  components: {
    NavigationBar,
    Keeps,
    BottomBar
  },
  methods: {
    getKeeps(id) {
      this.$store.dispatch("getKeeps", id);
    }
  }
};
</script>