<template>
    <v-dialog v-model="showProfile" fullscreen transition="slide-y-transition">
        <v-btn @click="getUsersKeeps()" slot="activator" flat fab>
            <v-icon>fas fa-user</v-icon>
        </v-btn>
        <v-toolbar app color="amber darken-4">
            <v-spacer></v-spacer>
            <v-btn @click="closeProfile()" fab flat>
                <v-icon>close</v-icon>
            </v-btn>
        </v-toolbar>
        <v-container id="h2" fill-height fluid class="blue-grey lighten-5">
            <v-layout align-start column>
                <h1 class="mt-5 pa-1">{{user.username}}</h1>
                <v-expansion-panel class="mt-3">
                    <v-expansion-panel-content class="blue-grey lighten-4">
                        <h3 slot="header" class="pa-2">you've authored {{usersKeeps.length}} keeps</h3>
                        <v-layout align-center wrap>
                            <v-flex xs12 sm12 md2 pa-1 v-for="keep in usersKeeps" :key="keep.id">
                                <keeps :keep="keep" />
                            </v-flex>
                        </v-layout>
                    </v-expansion-panel-content>
                </v-expansion-panel>
                <h2 class="pa-3">your vaults</h2>
                
            </v-layout>
        </v-container>
    </v-dialog>
</template>
<script>
import Keeps from "@/components/Keeps";
import Vaults from "@/components/Vaults";
export default {
  name: "profile",
  data() {
    return {
      showProfile: false
    };
  },
  computed: {
    user() {
      return this.$store.state.user;
    },
    usersKeeps() {
      return this.$store.state.usersKeeps;
    }
  },
  components: {
    Keeps,
    Vaults
  },
  methods: {
    getUsersKeeps() {
      this.$store.dispatch("getUsersKeeps", this.user.id);
    },
    closeProfile() {
      this.showProfile = false;
      this.$store.commit("setUsersKeeps", []);
    }
  }
};
</script>
<style>
#h2 {
    overflow-y: scroll;
}
</style>